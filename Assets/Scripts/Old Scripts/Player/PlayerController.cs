using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public enum ControlMoveType {Ground,Vertical }
    public ControlMoveType controlMoveType;


    [SerializeField] private Transform cameraOnPlayer;

    [SerializeField] private float SensX = 5, SensY = 10;
    [SerializeField] private Vector2 MinMax_Y = new Vector2(-40, 40);

    [SerializeField] private PlayerWeaponChanger weaponChanger;

    [SerializeField] private PlayerUI playerUI;


    [SerializeField] private List<GunPush> gunPushes;


    [SerializeReference] private APlayerMovement movement;
    private PlayerMovement PM;

    public delegate void PlayerWeaponControlHelper(AWeapon.WeaponState controlType);
    public event PlayerWeaponControlHelper PlayerWeaponControlEvent;


    private PlayerInput input;
    private bool isShiftNotInput = true;
    private float moveX, moveY;
    // private AWeapon weaponHealing;

    [SerializeField] private PhysicMaterial playerPhysMaterial;

    public List<GunPush> GunPushes
    {
        get
        {
            return gunPushes;
        }
    }


    private PlayerController()
    {
        PlayerInformation.GetInstance().PlayerController = this;     
    }

    private void Awake()
    {
        PlayerInformation.GetInstance().Player = gameObject;
        PM = FindObjectOfType<PlayerMovement>();
        input = new PlayerInput();
        
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

        switch(controlMoveType)
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
        var d = new Vector3(moveDir.x, moveDir.y, 0).normalized;

        //в дальнейшем лучше убрать в класс PlayerMovement
        transform.Translate(d * PM.VerticalAcceleration * Time.deltaTime);
        PM.body.isKinematic = true;
    }

    private void PlayerGroundMovement()
    {
        var moveDirection = input.MovementInput.GetDirection.ReadValue<Vector2>();

        var correctMove = new Vector3(moveDirection.x, 0, moveDirection.y).normalized;
        correctMove = transform.TransformDirection(correctMove);
        movement.Move(correctMove);

        //в дальнейшем лучше убрать в класс PlayerMovement
        PM.body.isKinematic = false;
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


    private void WeaponChecker()
    {
     
        //лучше подписаться на стейты самого оружия игрока

        //switch (weaponChanger.CurrentWeapon.State)
        //{
        //    case AWeapon.WeaponState.Attack:
        //        PlayerWeaponControlEvent?.Invoke(AWeapon.WeaponState.Attack);
        //        break;
        //    case AWeapon.WeaponState.Reload:
        //        PlayerWeaponControlEvent?.Invoke(AWeapon.WeaponState.Reload);
        //        break;
        //    case AWeapon.WeaponState.ImposibleAttack:
        //        PlayerWeaponControlEvent?.Invoke(AWeapon.WeaponState.ImposibleAttack);
        //        break;
        //    case AWeapon.WeaponState.Serenity:
        //        PlayerWeaponControlEvent?.Invoke(AWeapon.WeaponState.Serenity);
        //        break;
        //}

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
