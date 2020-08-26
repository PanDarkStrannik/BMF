using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(APlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraOnPlayer;

    [SerializeField] private float SensX = 5, SensY = 10;
    [SerializeField] private Vector2 MinMax_Y = new Vector2(-40, 40);

    [SerializeField] private WeaponChanger weaponChanger;

    [SerializeField] private PlayerUI playerUI;

    [SerializeField] private Blink blinkAbility;


    private APlayerMovement movement;
    private PlayerInput input;

    private bool isShiftNotInput = true;

    private float moveX, moveY;
   
    



    private void Awake()
    {
        input = new PlayerInput();
        movement = GetComponent<APlayerMovement>();
        weaponChanger.ChangeWeapon(0);
    }

    void Start()
    {
        ButtonsInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        RotationInput();
    }


    private void FixedUpdate()
    {
        var moveDirection = input.MovementInput.GetDirection.ReadValue<Vector2>();
        var correctMove = new Vector3(moveDirection.x, input.ButtonInputs.Jump.ReadValue<float>(), moveDirection.y);
        correctMove = transform.TransformDirection(correctMove);
        movement.Move(correctMove);
    }



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


    private void ButtonsInput()
    {
        input.ButtonInputs.Shoot.performed += context =>
        {

            weaponChanger.CurrentWeapon.Attack();
        };


        input.ButtonInputs.ChangeWeapon.performed += context => weaponChanger.ChangeWeapon(input.ButtonInputs.ChangeWeapon.ReadValue<float>());


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
        };

        input.ButtonInputs.Blink.performed += context =>
        {
            if(!blinkAbility.IsAttack)
            {
                blinkAbility.Attack();
                StartCoroutine(playerUI.ReloadTP(blinkAbility.ReloadTime));
            }
        };

    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle -= 360F;
        if (angle > 360F) angle += 360F;
        return Mathf.Clamp(angle, min, max);
    }

}

