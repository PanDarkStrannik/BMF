using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;
using DG.Tweening;
using StateMechanic;

[System.Serializable]
public class UnityBoolEvent : UnityEvent<bool> { }

public partial class PlayerController : AController
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
    [SerializeReference] private APlayerMovement movement;
    [SerializeField] private AAbility ability;
    [SerializeField] private List<GunPush> gunPushes;


    private PlayerMovement playerMovement;
    private AudioProvider audioProvider;
    private AInteractable interactable = null;
    private InputController inputController;



    [Header("Vertical Movement Settings")]
    [SerializeField] private float angle = 45f;
    [SerializeField] private float minTransformYToJump;
    [SerializeField] private float ropeJumpForce = 8f;
    private float arcSin = 0f;

    [Header("Other Components")]
    [SerializeField] private PhysicMaterial playerPhysMaterial;
    [SerializeField] private LayerMask whatIsReloadZone;

    private Vector3 movementDirection;
    private Vector3 rotationDirection;

    #region EVENTS
    public delegate void PlayerWeaponControlHelper(AWeapon.WeaponState controlType);
    public event PlayerWeaponControlHelper PlayerWeaponControlEvent;
    public event Action<PlayerWeaponChanger.WeaponSpellsHolder> OnChangeWeapon;
    public event Action<int> OnCurrentWeaponNumber;
    public event Action<Vector3, bool> OnPlayerMoved;
    public event Action OnPlayerInteractedSet;
    public event Action<AAbility> OnAbilityTook;
    public event Action OnAbilityNull;
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
    public AAbility Ability { get => ability; }
    public Transform CameraOnPlayer { get => cameraOnPlayer; }
    public float MouseMoveX { get => mouseMoveX; }
    public float MouseMoveY { get => mouseMoveY; }
    public Vector3 MovementDirection { get => movementDirection; set => movementDirection = value; }
    public PlayerWeaponChanger WeaponChanger { get => weaponChanger; }
    public List<GunPush> GunPush { get => gunPushes; }
    public APlayerMovement Movement { get => movement; }
    public bool IsReadyToChangeWeapon { get => isReadyToChangeWeapon; }
    public bool IsShiftNotInput { get => isShiftNotInput; set => isShiftNotInput = value; }
    public  PlayerMovement PlayerMovement { get => playerMovement; }
    public float MinTransformYToJump { get => minTransformYToJump; }
    public float RopeJumpForce { get => minTransformYToJump; }
    #endregion




    private PlayerController()
    {
        PlayerInformation.GetInstance().PlayerController = this;     
    }

    private void Awake()
    {
        inputController = GetComponent<InputController>();
        PlayerInformation.GetInstance().Player = gameObject;

        playerMovement = GetComponentInChildren<PlayerMovement>();
        audioProvider = GetComponentInChildren<AudioProvider>();
    }

    void Start()
    {
        weaponChanger.ChangeWeapon(0);
        inputController.ButtonInputSetup();
    }

    private void OnEnable()
    {
        inputController.Player = this;
        inputController.OnNullAbility += InputController_OnNullAbility;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void OnDisable()
    {
        inputController.OnNullAbility -= InputController_OnNullAbility;

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
        //var cameraRot = cameraOnPlayer.transform.forward;

        //var moveDir = input.MovementInput.GetDirection.ReadValue<Vector2>();
        //var d = new Vector3(moveDir.x, moveDir.y, 0).normalized;

        //cameraRot = transform.InverseTransformDirection(cameraRot);

        //arcSin = Mathf.Asin(cameraRot.y) * Mathf.Rad2Deg;

        //if (Math.Abs(arcSin) < angle)
        //{
        //    d = new Vector3(d.x, 0, d.y);
        //    VerticalJump(d, moveDir);
        //}
        //else
        //{
        //    d = new Vector3(d.x, d.y * cameraRot.y, d.z * cameraRot.z);
        //}
        
        //playerMovement.VerticalMove(d);
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
        movementDirection = inputController.MovementDirectionInput();

        var correctMove = new Vector3(movementDirection.x, 0, movementDirection.y).normalized;

        correctMove = transform.TransformDirection(correctMove);
        movement.Move(correctMove);
        
        OnPlayerMoved?.Invoke(movementDirection, isShiftNotInput);
    }



    private void SlopeFriction()
    {
        if(movementDirection == Vector3.zero && movement.Grounded)
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
        rotationDirection = inputController.RotationDirectionInput();

        mouseMoveY -= rotationDirection.y * SensY * Time.deltaTime;
        mouseMoveY = ClampAngle(mouseMoveY, MinMax_Y.x, MinMax_Y.y);

        mouseMoveX = transform.rotation.eulerAngles.y + rotationDirection.x * SensX * Time.deltaTime;

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

    public void InvokeChangeWeaponEvent(PlayerWeaponChanger.WeaponSpellsHolder weapon)
    {
        OnChangeWeapon?.Invoke(weapon);
    }

    public void InvokeOnCurrentWeapon(int num)
    {
        OnCurrentWeaponNumber?.Invoke(num);
    }

    #region Перенести куда-нибудь в будущем
    public void SetAbility(AAbility newAbility)
    {
        if (ability != null) return;

        ability = newAbility;
        OnAbilityTook?.Invoke(newAbility);
    }

    
    public void NullAbility()
    {
        ability = null;
        OnAbilityNull?.Invoke();
    }

    private void InputController_OnNullAbility()
    {
        OnAbilityNull?.Invoke();
    }

    #endregion

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle -= 360F;
        if (angle > 360F) angle += 360F;
        return Mathf.Clamp(angle, min, max);
    }

}
