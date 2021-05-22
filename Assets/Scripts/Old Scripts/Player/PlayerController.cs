using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;
using DG.Tweening;


[System.Serializable]
public class UnityBoolEvent : UnityEvent<bool> { }

public class PlayerController : MonoBehaviour
{
    public enum ControlMoveType {Ground,Vertical }
    public ControlMoveType controlMoveType;

    [Header("Camera Settings")]
    [SerializeField] private Transform cameraOnPlayer;
    [SerializeField][Range(0,10)] private float SensX = 5;
    [SerializeField][Range(0,10)] private float SensY = 5;
    [Range(0, 2)] [SerializeField] private float sideWaysAngle;
    [SerializeField] private Vector2 MinMax_Y = new Vector2(-40, 40);
    private float mouseMoveX, mouseMoveY;

    [Header("Ref's to other Classes")]
    [SerializeField] private PlayerWeaponChanger weaponChanger;
    [SerializeField] private PlayerUI playerUI;
    [SerializeReference] private APlayerMovement movement;
    [SerializeField] private List<AAbility> abilities;
    [SerializeField] private List<GunPush> gunPushes;
    private PlayerMovement playerMovement;
    private AudioProvider audioProvider;
    private PlayerInput input;
    private AInteractable interactable = null;



    [Header("Vertical Movement Settings")]
    [SerializeField] private float angle = 45f;
    [SerializeField] private float minTransformYToJump;
    [SerializeField] private float ropeJumpForce = 8f;
    private float arcSin = 0f;

    [Header("Other Components")]
    [SerializeField] private PhysicMaterial playerPhysMaterial;
    [SerializeField] private LayerMask whatIsReloadZone;

    private Vector3 movementDirection;

    #region EVENTS
    public delegate void PlayerWeaponControlHelper(AWeapon.WeaponState controlType);
    public event PlayerWeaponControlHelper PlayerWeaponControlEvent;
    public event Action<PlayerWeaponChanger.WeaponSpellsHolder> OnChangeWeapon;
    public event Action<int> OnCurrentWeaponNumber;
    public event Action<Vector3, bool> OnPlayerMoved;
    public event Action OnPlayerInteractedSet;
    #endregion

    private bool isShiftNotInput = true;
    private bool isReadyToChangeWeapon = true;

    #region PROPERITES
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

    public List<GunPush> GunPushes
    {
        get
        {
            return gunPushes;
        }
    }

    public AudioProvider AudioProvider { get => audioProvider; }
    public List<AAbility> Abilities { get => abilities; }
    public Transform CameraOnPlayer { get => cameraOnPlayer; }
    public float MouseMoveX { get => mouseMoveX; }
    public float MouseMoveY { get => mouseMoveY; }
    public Vector3 MovementDirection { get => movementDirection; }
    #endregion



    private PlayerController()
    {
        PlayerInformation.GetInstance().PlayerController = this;     
    }

    private void Awake()
    {
        input = new PlayerInput();
        PlayerInformation.GetInstance().Player = gameObject;

        playerMovement = GetComponentInChildren<PlayerMovement>();
        audioProvider = GetComponentInChildren<AudioProvider>();
    }

    void Start()
    {
        weaponChanger.ChangeWeapon(0);
        ButtonsInput();
    }

    private void OnEnable()
    {
        input.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        input.Disable();
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    void Update()
    {
        switch (controlMoveType)
        {
            case ControlMoveType.Ground:
                RotationInput();
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
        var cameraRot = cameraOnPlayer.transform.forward;

        var moveDir = input.MovementInput.GetDirection.ReadValue<Vector2>();
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
        
        playerMovement.VerticalMove(d);
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
        movementDirection = input.MovementInput.GetDirection.ReadValue<Vector2>();

        var correctMove = new Vector3(movementDirection.x, 0, movementDirection.y).normalized;

        correctMove = transform.TransformDirection(correctMove);
        movement.Move(correctMove);
        
        OnPlayerMoved?.Invoke(movementDirection, isShiftNotInput);
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

        mouseMoveY -= rotationInput.y * SensY * Time.deltaTime;
        mouseMoveY = ClampAngle(mouseMoveY, MinMax_Y.x, MinMax_Y.y);

        mouseMoveX = transform.rotation.eulerAngles.y + rotationInput.x * SensX * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, mouseMoveX, 0);

        if (cameraOnPlayer != null)
        {
            cameraOnPlayer.rotation = Quaternion.Euler(mouseMoveY, cameraOnPlayer.eulerAngles.y, 0);
             CameraSideAngles();
        }

    }

    public void SetSensX(float amount)
    {
        SensX = amount;
    }

    public void SetSensY(float amount)
    {
        SensY = amount;
    }

    private void CameraSideAngles()
    {

        if (movementDirection.x > 0)
        {
           cameraOnPlayer.DORotate(new Vector3(cameraOnPlayer.eulerAngles.x, mouseMoveX, -sideWaysAngle), 0.1f);
        }
        else if (movementDirection.x < 0)
        {
            cameraOnPlayer.DORotate(new Vector3(cameraOnPlayer.eulerAngles.x, mouseMoveX, sideWaysAngle), 0.1f);
        }
        else
        {
           cameraOnPlayer.DORotate(new Vector3(cameraOnPlayer.eulerAngles.x, mouseMoveX, 0f), 0.2f);
        }
    }

    public void StopChangingWeapon(float time)
    {
        StartCoroutine(StopWeaponChange(time));
    }

    private IEnumerator StopWeaponChange(float time)
    {
        isReadyToChangeWeapon = false;
        yield return new WaitForSeconds(time);
        isReadyToChangeWeapon = true;
    }




    private void ButtonsInput()
    {
        input.ButtonInputs.MainAttack.performed += context =>
        {
            if(!PauseController.isPaused)
            {
                if (weaponChanger.CurrentWeapon.TryUseFirstWeapon())
                {
                    foreach (var e in gunPushes)
                    {
                        if (weaponChanger.CurrentWeapon.Weapon1.WeaponType == e.WeaponType)
                        {
                            var push = transform.TransformDirection(e.PushForce);
                           // StartCoroutine(movement.ImpulseMove(push, e.ForceMode, e.TimeToPush));
                            e.ShakingParams.ShakeEventInvoke();
                        }
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
            if(!PauseController.isPaused)
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
            }
        };



        input.ButtonInputs.ChangeWeaponByKeyboard.performed += context =>
        {
            if(!PauseController.isPaused)
            {
               if(isReadyToChangeWeapon)
               {
                  weaponChanger.ChangeWeapon((int)input.ButtonInputs.ChangeWeaponByKeyboard.ReadValue<float>());
                  if(weaponChanger.CurrentWeapon != null)
                  {
                      OnChangeWeapon?.Invoke(weaponChanger.CurrentWeapon);
                      OnCurrentWeaponNumber?.Invoke(weaponChanger.CurrentWeaponNum);
                  }
               }
            }
        };

        input.ButtonInputs.MouseScroll.performed += context =>
        {
            if(!PauseController.isPaused)
            {
                if(isReadyToChangeWeapon)
                {
                    var testValue = input.ButtonInputs.MouseScroll.ReadValue<float>();
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
                        OnCurrentWeaponNumber?.Invoke(weaponChanger.CurrentWeaponNum);
                    }
                }
            }
        };

        input.ButtonInputs.Jump.performed += context =>
        {
            if(!PauseController.isPaused)
            {
                if (movement.Grounded)
                {
                    movement.body.velocity = playerMovement.JumpForce * Vector3.up;
                    audioProvider.PlayOneShot("Jump");
                }
            }
        };

        input.ButtonInputs.Ability1.performed += _ =>
        {
            if(!PauseController.isPaused)
            {
                foreach (var a in abilities)
                {
                    if(a is Mel)
                    {
                        if (a.AbilityState == AbilityState.Enabled)
                            a.UseAbility();
                    }
                }
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
