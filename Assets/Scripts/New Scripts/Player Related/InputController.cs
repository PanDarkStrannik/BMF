﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class InputController : MonoBehaviour
{
    private PlayerInput input;
    private PlayerController player;

    public PlayerController Player { get => player; set => player = value; }



    private void Awake()
    {
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }


    public void ButtonInputSetup()
    {
        if (!PauseController.isPaused)
        {
            HandleMainAttack();
            HandleSecondAttack();
            HandleWeaponChangeScroll();
            HandleWeaponChangeKeyboard();
            HandleJump();
            HandleIntrecation();
            HandleAbillity1();
            HandleMoveSpeedChange();
        }
    }

    #region VECTORS

    public Vector3 MovementDirectionInput()
    {
        return input.MovementInput.GetDirection.ReadValue<Vector2>();
    }

    public  Vector3 RotationDirectionInput()
    {
        return input.RotationInput.GetRotation.ReadValue<Vector2>();
    }

    #endregion

    #region MOUSE

    private void HandleMainAttack()
    {
        input.ButtonInputs.MainAttack.performed += context =>
        {
            if (Player.WeaponChanger.CurrentWeapon.TryUseFirstWeapon())
            {
                foreach (var e in player.GunPush)
                {
                    if (player.WeaponChanger.CurrentWeapon.Weapon1.WeaponType == e.WeaponType)
                    {
                        var push = transform.TransformDirection(e.PushForce);
                        e.ShakingParams.ShakeEventInvoke();
                    }
                }
            }
        };
    }

    private void HandleSecondAttack()
    {
        input.ButtonInputs.SecondAttack.performed += context =>
        {
            if(!player.WeaponChanger.CurrentWeapon.Weapon2.IsWeaponCharged)
            {
                player.WeaponChanger.CurrentWeapon.TryUseSecondWeapon();
                player.WeaponChanger.CurrentWeapon.Weapon2.IsWeaponCharged = true;
            }
            else
            {
                player.WeaponChanger.CurrentWeapon.Weapon2.IsWeaponCharged = false;
            }
               

        };
    }


    private void HandleWeaponChangeScroll()
    {
        input.ButtonInputs.MouseScroll.performed += context =>
        {
                if (player.IsReadyToChangeWeapon)
                {
                    var testValue = input.ButtonInputs.MouseScroll.ReadValue<float>();
                    if (testValue > 0)
                    {
                        player.WeaponChanger.NextWeapon();
                    }
                    else if (testValue < 0)
                    {
                        player.WeaponChanger.PrevWeapon();
                    }
                    else
                    {
                        throw new System.Exception("Почему-то при скролле выдало 0");
                    }
                    if (player.WeaponChanger.CurrentWeapon != null)
                    {
                        player.InvokeChangeWeaponEvent(player.WeaponChanger.CurrentWeapon);
                        player.InvokeOnCurrentWeapon(player.WeaponChanger.CurrentWeaponNum);
                    }
                }
        };
    }



    #endregion

    #region KEYBOARD

    private void HandleWeaponChangeKeyboard()
    {
        input.ButtonInputs.ChangeWeaponByKeyboard.performed += context =>
        {
                if (player.IsReadyToChangeWeapon)
                {
                    player.WeaponChanger.ChangeWeapon((int)input.ButtonInputs.ChangeWeaponByKeyboard.ReadValue<float>());

                    if (player.WeaponChanger.CurrentWeapon != null)
                    {
                        player.InvokeChangeWeaponEvent(player.WeaponChanger.CurrentWeapon);
                        player.InvokeOnCurrentWeapon(player.WeaponChanger.CurrentWeaponNum);
                    }
                }
        };
    }

    private void HandleJump()
    {
        input.ButtonInputs.Jump.performed += context =>
        {
                if (player.Movement.Grounded)
                {
                    player.Movement.body.velocity = player.PlayerMovement.JumpForce * Vector3.up;
                    player.AudioProvider.PlayOneShot("Jump");
                }
        };
    }


    private void HandleMoveSpeedChange()
    {
        input.ButtonInputs.ChangeSpeed.performed += context =>
        {

            if (player.IsShiftNotInput)
            {
                player.Movement.moveType = APlayerMovement.PlayerMoveType.Fast;
                player.IsShiftNotInput = false;

            }
            else
            {
                player.Movement.moveType = APlayerMovement.PlayerMoveType.Slow;
                player.IsShiftNotInput = true;

            }
        };
    }

    private void HandleAbillity1()
    {
        input.ButtonInputs.Ability1.performed += _ =>
        {
                if (player.Ability1.AbilityState == AbilityState.Enabled)
                {
                     player.Ability1.UseAbility();
                }
        };
    }

    private void HandleIntrecation()
    {
        input.ButtonInputs.Interact.performed += _ =>
        {
            if (player.Interactable != null)
            {
                player.Interactable.Use();
            }
        };
    }

    #endregion

}