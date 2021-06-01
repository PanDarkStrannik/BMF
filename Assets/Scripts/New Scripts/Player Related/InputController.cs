using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController 
{
    private PlayerInput input;

    private bool isLightAttackTriggered = false;
    private bool isStrongAttackTriggered = false;

    public InputController(PlayerInput input)
    {
        this.input = input;
    }
   

    public void InputSetup(PlayerWeaponChanger weaponChanger)
    {
        MouseLeftClick(weaponChanger);
        MouseLeftHold();
    }


    private void MouseLeftClick(PlayerWeaponChanger weaponChanger)
    {
        input.ButtonInputs.MainAttack.performed += _ =>
        {
            isLightAttackTriggered = true;
            Debug.Log("LightAttack");
        };
    }

   private void MouseLeftHold()
    {
        input.ButtonInputs.MainStrongAttack.performed += _ =>
        {
            isStrongAttackTriggered = true;
            Debug.Log("Holding..");
        };

        if(isStrongAttackTriggered)
        {
           input.ButtonInputs.MainStrongAttack.canceled += _ =>
           {
               Debug.Log("Release..");
               isStrongAttackTriggered = false;
           };
        }

    }
}
