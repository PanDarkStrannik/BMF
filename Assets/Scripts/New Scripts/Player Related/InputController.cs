using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController 
{
    private PlayerInput input;

   

    public void InputSetup()
    {
        input = new PlayerInput();

        if(!PauseController.isPaused)
        {
            MouseLeftClick();
        }
    }

    public bool MouseLeftClick()
    {
       if(input.ButtonInputs.MainAttack.triggered)
       {
           return true;
       }

          return false;
    }

    private void MouseRightClick(PlayerWeaponChanger weaponChanger)
    {

    }

    private void MouseLeftHold()
    {

    }

    private void MouseRightHold()
    {

    }

    private void ShiftHold()
    {

    }
}
