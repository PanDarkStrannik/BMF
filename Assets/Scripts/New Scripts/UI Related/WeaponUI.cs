using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Text ammoAmout;

    public float waterValue = 0f;

    private void OnEnable()
    {
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged += UpdateAmmo;
        waterValue = PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.typesValues[DamagebleParam.ParamType.HolyWater];

        PlayerInformation.GetInstance().PlayerController.OnChangeWeapon += PlayerController_OnChangeWeapon;
    }

    private void PlayerController_OnChangeWeapon(PlayerWeaponChanger.WeaponSpellsHolder obj)
    {
        Debug.Log($"Первое оружие: {obj.Weapon1.name}, Второе оружие: {obj.Weapon2.name}");
    }

    private void OnDisable()
    {
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged -= UpdateAmmo;
        PlayerInformation.GetInstance().PlayerController.OnChangeWeapon -= PlayerController_OnChangeWeapon;
    }


    public void UpdateAmmo(DamagebleParam.ParamType paramType, float value, float maxValue)
    {

      if(paramType == DamagebleParam.ParamType.HolyWater)
        {
            waterValue = value;
           ammoAmout.text = value.ToString();
        }
    }

   

}
