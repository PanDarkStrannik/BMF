using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraOnPlayer;

    [SerializeField] private float SensX = 5, SensY = 10;
    [SerializeField] private Vector2 MinMax_Y = new Vector2(-40, 40);

    [SerializeField] private WeaponChanger weaponChanger;

    [SerializeField] private PlayerUI playerUI;

    //[SerializeField] private Blink blinkAbility;

    [SerializeField] private List<GunPush> gunPushes;


    [SerializeReference] private APlayerMovement movement;

    public delegate void PlayerWeaponControlHelper(AWeapon.WeaponState controlType);
    public event PlayerWeaponControlHelper PlayerWeaponControlEvent;



    private PlayerInput input;
    private bool isShiftNotInput = true;
    private float moveX, moveY;
    private WeaponHealing weaponHealing;



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
        input = new PlayerInput();
        //movement = GetComponent<APlayerMovement>();
        weaponChanger.ChangeWeapon(0);
    }

    void Start()
    {
        ButtonsInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        weaponHealing = weaponChanger.AllWeapons.Find(x => x is WeaponHealing) as WeaponHealing; 
        
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
        WeaponChecker();
        RotationInput();
        var moveDirection = input.MovementInput.GetDirection.ReadValue<Vector2>();
        var correctMove = new Vector3(moveDirection.x, input.ButtonInputs.Jump.ReadValue<float>(), moveDirection.y);
        correctMove = transform.TransformDirection(correctMove);
        movement.Move(correctMove);
    }


    //private void FixedUpdate()
    //{
    //    var moveDirection = input.MovementInput.GetDirection.ReadValue<Vector2>();
    //    var correctMove = new Vector3(moveDirection.x, input.ButtonInputs.Jump.ReadValue<float>(), moveDirection.y);
    //    correctMove = transform.TransformDirection(correctMove);
    //    movement.Move(correctMove);
    //}



    private void RotationInput()
    {
        var rotationInput = input.RotationInput.GetRotation.ReadValue<Vector2>();

        moveY -= rotationInput.y * SensY;
        moveY = ClampAngle(moveY, MinMax_Y.x, MinMax_Y.y);

        moveX = transform.rotation.eulerAngles.y + rotationInput.x * SensX;

        transform.rotation = Quaternion.Euler(0, moveX, 0);

        if (cameraOnPlayer != null)
        {
            cameraOnPlayer.rotation = Quaternion.Euler(moveY, cameraOnPlayer.eulerAngles.y, 0);
        }

    }


    private void WeaponChecker()
    {
     

        switch (weaponChanger.CurrentWeapon.State)
        {
            case AWeapon.WeaponState.Attack:
                PlayerWeaponControlEvent?.Invoke(AWeapon.WeaponState.Attack);
                break;
            case AWeapon.WeaponState.Reload:
                PlayerWeaponControlEvent?.Invoke(AWeapon.WeaponState.Reload);
                break;
            case AWeapon.WeaponState.ImposibleAttack:
                PlayerWeaponControlEvent?.Invoke(AWeapon.WeaponState.ImposibleAttack);
                break;
            case AWeapon.WeaponState.Serenity:
                PlayerWeaponControlEvent?.Invoke(AWeapon.WeaponState.Serenity);
                break;
        }

    }

    private void ButtonsInput()
    {
        input.ButtonInputs.Shoot.performed += context =>
        {
            if (weaponChanger.CurrentWeapon.State == AWeapon.WeaponState.Serenity)
            {
                foreach (var e in gunPushes)
                {
                    if (weaponChanger.CurrentWeapon.WeaponType == e.WeaponType)
                    {
                        var push = transform.TransformDirection(e.PushForce);
                        StartCoroutine(movement.ImpulseMove(push, e.ForceMode, e.TimeToPush));
                        e.ShakingParams.ShakeEventInvoke();
                    }
                }
            }
            weaponChanger.CurrentWeapon.Attack();
        };



        input.ButtonInputs.ChangeWeaponByKeyboard.performed += context =>
        {
            weaponChanger.ChangeWeapon(input.ButtonInputs.ChangeWeaponByKeyboard.ReadValue<float>());
            ChangeAbilityBecauseWeapon(weaponChanger.CurrentWeapon.WeaponType);
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
            //weaponChanger.ChangeWeapon(input.ButtonInputs.ChangeWeaponByMouse.ReadValue<Vector2>().y);
            //ChangeAbilityBecauseWeapon(weaponChanger.CurrentWeapon.WeaponType);
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
            ChangeAbilityBecauseWeapon(weaponChanger.CurrentWeapon.WeaponType);

        };


        input.MovementInput.GetDirection.performed += context =>
        {
            ChangeAbilityBecauseWeapon(weaponChanger.CurrentWeapon.WeaponType);
        };

        input.ButtonInputs.Blink.performed += context =>
        {
            if (weaponHealing.State == AWeapon.WeaponState.Serenity)
            {
                weaponHealing.Attack(gameObject);
            }
            
            //if(!blinkAbility.IsAttack)
            //{
            //    blinkAbility.Attack();
            //   // StartCoroutine(playerUI.ReloadTP(blinkAbility.ReloadTime));
            //}
        };

       

    }


    private bool ChangeAbilityBecauseWeapon(WeaponType type)
    {
        switch(type)
        {

            case WeaponType.Range:
                movement.moveType = APlayerMovement.PlayerMoveType.RangeMove;
                return true;
        }
        return false;
    }


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
