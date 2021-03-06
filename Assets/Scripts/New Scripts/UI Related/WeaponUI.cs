using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Text ammoAmout;
    [SerializeField] private Text reloadHint;
    [SerializeField] private Text textReload;
    [SerializeField] private WeaponRange rangeWeapon;


    private float waterValue;
    private float maxWaterValue;
    private float currentReloadingTime;
    public float maxReloadingTime;


    private void Awake()
    {
        currentReloadingTime = rangeWeapon.attackParametres.ReloadTime;
        waterValue = PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.typesValues[DamagebleParam.ParamType.HolyWater];
        maxWaterValue = PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.typesMaxValues[DamagebleParam.ParamType.HolyWater];
    }


    private void Update()
    {
        // потом переделать по-человечекси  \/
        CheckReloaZone();
    }

    private void CheckReloaZone()
    {
        if (PlayerInformation.GetInstance().PlayerController.IsReadyToReload && waterValue < maxWaterValue)
        {
            reloadHint.text = "Pres R to Reload";
        }
        else if (PlayerInformation.GetInstance().PlayerController.IsReadyToReload && waterValue >= maxWaterValue)
        {
            reloadHint.text = "Ammo is full";
        }
        else
        {
            reloadHint.text = null;
        }
    }

   
    private void OnEnable()
    {
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged += UpdateAmmo;
        PlayerInformation.GetInstance().PlayerController.OnChangeWeapon += PlayerController_OnChangeWeapon;
    }


    private void OnDisable()
    {
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged -= UpdateAmmo;
        PlayerInformation.GetInstance().PlayerController.OnChangeWeapon -= PlayerController_OnChangeWeapon;
    }

    private void PlayerController_OnChangeWeapon(PlayerWeaponChanger.WeaponSpellsHolder obj)
    {
       if(obj.Weapon1.WeaponType == WeaponType.Mili)
        {
            ammoAmout.enabled = false;
        }

       if(obj.Weapon1.WeaponType == WeaponType.Range)
        {
            ammoAmout.enabled = true;
            ammoAmout.text = waterValue.ToString();
        }
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
