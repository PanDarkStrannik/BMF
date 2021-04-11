using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


[System.Serializable]
public class UnityBoolEvent : UnityEvent<bool> { }

public class PlayerController : MonoBehaviour
{
    public enum ControlMoveType {Ground,Vertical }
    public ControlMoveType controlMoveType;

    [Header("Camera Settings")]
    [SerializeField] private Transform cameraOnPlayer;
    [SerializeField] private float SensX = 5, SensY = 10;
    [SerializeField] private Vector2 MinMax_Y = new Vector2(-40, 40);
    private float moveX, moveY;

    [Header("Ref's to other Classes")]
    [SerializeField] private PlayerWeaponChanger weaponChanger;
    [SerializeField] private PlayerUI playerUI;
    [SerializeReference] private APlayerMovement movement;
    [SerializeField] private List<GunPush> gunPushes;
    private WeaponRange rangeW;
    private PlayerMovement PM;
    private PlayerInput input;
    private IRay rayCreation;
    private AInteractable interactable = null;

    public AInteractable Interactable 
    {
        get { return interactable; }

        set 
        {
            interactable = value;
            if(interactable != null)
            {
             OnPlayerInteractedSet?.Invoke();
            }
        }

    }


    [Header("Vertical Movement Settings")]
    [SerializeField] private float angle = 45f;
    [SerializeField] private float minTransformYToJump;
    [SerializeField] private float ropeJumpForce = 8f;
    private float arcSin = 0f;

    [Header("Other Components")]
    [SerializeField] private PhysicMaterial playerPhysMaterial;
    [SerializeField] private LayerMask whatIsReloadZone;

    //Events
    public delegate void PlayerWeaponControlHelper(AWeapon.WeaponState controlType);
    public event PlayerWeaponControlHelper PlayerWeaponControlEvent;
    public event Action<PlayerWeaponChanger.WeaponSpellsHolder> OnChangeWeapon;
    public event Action<Vector3> OnPlayerMoved;
    public event Action OnPlayerInteractedSet;

    //bools
    private bool isShiftNotInput = true;
    private bool isReadyToReload = false;

    //properties
    public List<GunPush> GunPushes
    {
        get
        {
            return gunPushes;
        }
    }

    public bool IsReadyToReload { get => isReadyToReload; }



    // private AWeapon weaponHealing;




    private PlayerController()
    {
        PlayerInformation.GetInstance().PlayerController = this;     
    }

    private void Awake()
    {
        PlayerInformation.GetInstance().Player = gameObject;

        PM = FindObjectOfType<PlayerMovement>();
        rangeW = FindObjectOfType<WeaponRange>();
        input = new PlayerInput();
        rayCreation = GetComponent<IRay>();
        
        weaponChanger.ChangeWeapon(0);
    }

    void Start()
    {
        ButtonsInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

      //  weaponHealing = weaponChanger.AllWeapons.Find(x => x is IHeallingWeapon) as AWeapon; 
        
    }

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }


    void Update()
    {
        // WeaponChecker();
       // CheckingReloadZone();

        switch (controlMoveType)
        {
            case ControlMoveType.Ground:
                RotationInput();
                PlayerJump();
                SlopeFriction();
                PlayerGroundMovement();
                break;

            case ControlMoveType.Vertical:
                RotationInput();
                PlayerVerticalMovement();
                break;
        }
    }

    private void PlayerVerticalMovement()
    {
        var moveDir = input.MovementInput.GetDirection.ReadValue<Vector2>();

        var cameraRot = cameraOnPlayer.transform.forward;

        var d = new Vector3(moveDir.x, moveDir.y, 0).normalized;

        cameraRot = transform.InverseTransformDirection(cameraRot);

        arcSin = Mathf.Asin(cameraRot.y) * Mathf.Rad2Deg;

        if (Math.Abs(arcSin) < angle)
        {
            d = new Vector3(d.x, 0, d.y);
            VerticalJump(d, moveDir);
        }
        else
        {
            d = new Vector3(d.x, d.y * cameraRot.y, d.z * cameraRot.z);
        }
        
        PM.VerticalMove(d);
    }

    private void VerticalJump(Vector3 d, Vector3 moveDir)
    {
        var temp = new Vector3(0, 0, d.z);
        if (temp != Vector3.zero && transform.position.y > minTransformYToJump)
        {
          StartCoroutine(movement.ImpulseMove(moveDir * ropeJumpForce, ForceMode.Impulse, 0));
        }
    }

    private void PlayerGroundMovement()
    {
        var moveDirection = input.MovementInput.GetDirection.ReadValue<Vector2>();

        var correctMove = new Vector3(moveDirection.x, 0, moveDirection.y).normalized;

        correctMove = transform.TransformDirection(correctMove);
        movement.Move(correctMove);
        OnPlayerMoved?.Invoke(moveDirection);
    }


    private void PlayerJump()
    {
        input.ButtonInputs.Jump.performed += context =>
        {
            
          if(movement.Grounded)
            {
             movement.body.velocity = PM.JumpForce * Vector3.up;
            }
        };
    }

    private void SlopeFriction()
    {
        var inputValue = input.MovementInput.GetDirection.ReadValue<Vector2>();
        if(inputValue == Vector2.zero && movement.Grounded)
        {
            playerPhysMaterial.frictionCombine = PhysicMaterialCombine.Average;
        }
        else
        {
            playerPhysMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        }

    }


    private void RotationInput()
    {
        var rotationInput = input.RotationInput.GetRotation.ReadValue<Vector2>();

        moveY -= rotationInput.y * SensY * Time.deltaTime;
        moveY = ClampAngle(moveY, MinMax_Y.x, MinMax_Y.y);

        moveX = transform.rotation.eulerAngles.y + rotationInput.x * SensX * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, moveX, 0);

        if (cameraOnPlayer != null)
        {
            cameraOnPlayer.rotation = Quaternion.Euler(moveY, cameraOnPlayer.eulerAngles.y, 0);
        }

    }

    

    private void ButtonsInput()
    {
        input.ButtonInputs.MainAttack.performed += context =>
        {
            if (weaponChanger.CurrentWeapon.TryUseFirstWeapon())
            {
                foreach (var e in gunPushes)
                {
                    if (weaponChanger.CurrentWeapon.Weapon1.WeaponType == e.WeaponType)
                    {
                        var push = transform.TransformDirection(e.PushForce);
                        StartCoroutine(movement.ImpulseMove(push, e.ForceMode, e.TimeToPush));
                        e.ShakingParams.ShakeEventInvoke();
                    }
                }
            }
            //if(weaponChanger.CurrentWeapon.TryReturnNeededWeaponType<IDamagingWeapon>(out IDamagingWeapon returnedWeapon))
            //{
            //    returnedWeapon.Attack();
            //}
         
        };

        input.ButtonInputs.SecondAttack.performed += context =>
        {
            if (weaponChanger.CurrentWeapon.TryUseSecoundWeapon())
            {
                foreach (var e in gunPushes)
                {
                    if (weaponChanger.CurrentWeapon.Weapon2.WeaponType == e.WeaponType)
                    {
                        var push = transform.TransformDirection(e.PushForce);
                        StartCoroutine(movement.ImpulseMove(push, e.ForceMode, e.TimeToPush));
                        e.ShakingParams.ShakeEventInvoke();
                    }
                }
            }
        };



        input.ButtonInputs.ChangeWeaponByKeyboard.performed += context =>
        {
            weaponChanger.ChangeWeapon((int)input.ButtonInputs.ChangeWeaponByKeyboard.ReadValue<float>());
            if(weaponChanger.CurrentWeapon != null)
            {
                OnChangeWeapon?.Invoke(weaponChanger.CurrentWeapon);
            }
         //   ChangeAbilityBecauseWeapon(weaponChanger.CurrentWeapon.WeaponType);
        };

        input.ButtonInputs.MouseScroll.performed += context =>
        {
            var testValue = input.ButtonInputs.MouseScroll.ReadValue<float>();
            Debug.Log(testValue);
            if (testValue > 0)
            {
                weaponChanger.NextWeapon();
            }
            else if (testValue < 0)
            {
                weaponChanger.PrevWeapon();
            }
            else
            {
                throw new System.Exception("Почему-то при скролле выдало 0");
            }
            if (weaponChanger.CurrentWeapon != null)
            {
                OnChangeWeapon?.Invoke(weaponChanger.CurrentWeapon);
            }
        };



        input.ButtonInputs.Reload.performed += _ =>
        {
            if(interactable != null)
            {
                interactable.Use();
            }
        };



        input.ButtonInputs.ChangeSpeed.performed += context =>
        {

            if (isShiftNotInput)
            {
                movement.moveType = APlayerMovement.PlayerMoveType.Fast;
                isShiftNotInput = false;

            }
            else
            {
                movement.moveType = APlayerMovement.PlayerMoveType.Slow;
                isShiftNotInput = true;

            }
           // ChangeAbilityBecauseWeapon(weaponChanger.CurrentWeapon.WeaponType);

        };


        //input.MovementInput.GetDirection.performed += context =>
        //{
        //   // ChangeAbilityBecauseWeapon(weaponChanger.CurrentWeapon.WeaponType);
        //};

        input.ButtonInputs.Blink.performed += context =>
        {
            //if (weaponHealing.State == AWeapon.WeaponState.Serenity)
            //{
            //    weaponHealing.TryReturnNeededWeaponType<IHeallingWeapon>(out IHeallingWeapon returnedObject);
            //    returnedObject.Heal(gameObject);
            //}
        };

       

    }




    //private bool ChangeAbilityBecauseWeapon(WeaponType type)
    //{
    //    switch(type)
    //    {

    //        case WeaponType.Range:
    //            movement.moveType = APlayerMovement.PlayerMoveType.RangeMove;
    //            return true;
    //    }
    //    return false;
    //}


    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle -= 360F;
        if (angle > 360F) angle += 360F;
        return Mathf.Clamp(angle, min, max);
    }

    [System.Serializable]
    public class GunPush
    {
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private Vector3 pushForce;
        [SerializeField] private ForceMode forceMode;
        [SerializeField] private float timeToPush;
        [SerializeField] private ShakingParams shakingParams;

        public WeaponType WeaponType
        {
            get
            {
                
                return weaponType;
            }
        }

        public Vector3 PushForce
        {
            get
            {
                return pushForce;
            }
        }

        public ForceMode ForceMode
        {
            get
            {
                return forceMode;
            }
        }

        public float TimeToPush
        {
            get
            {
                return timeToPush;
            }
        }

        public ShakingParams ShakingParams
        {
            get
            {
                return shakingParams;
            }
        }
    }

}
