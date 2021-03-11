using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Text ammoAmout;
    [SerializeField] private Text reloadHint;
    [SerializeField] private Image reloadImageTimer;
    [SerializeField] private WeaponRange rangeWeapon;


    private float waterValue;
    private float maxWaterValue;
    private float currentReloadingTime;
    private float maxReloadingTime;

    private bool isThisRangeWeapon;
    private bool isThisMeleeWeapon;

    private void Awake()
    {
        maxReloadingTime = rangeWeapon.attackParametres.ReloadTime;
        currentReloadingTime = maxReloadingTime;

        waterValue = PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.typesValues[DamagebleParam.ParamType.HolyWater];
        maxWaterValue = PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.typesMaxValues[DamagebleParam.ParamType.HolyWater];

    }


    private void Update()
    {
        CheckReloadZone();
       if(rangeWeapon.IsReloading)
        {
         UpdateReloadingTimer();
        }
    }

    private void CheckReloadZone()
    {
        bool isPlayerInRange = PlayerInformation.GetInstance().PlayerController.IsReadyToReload;

        if (isPlayerInRange && waterValue < maxWaterValue)
        {
            if(!isThisMeleeWeapon && isThisRangeWeapon)
            {
              reloadHint.text = "R - Наполнить кропильницу";
            }
            
        }
        else if(isPlayerInRange && waterValue >= maxWaterValue)
        {
            if (!isThisMeleeWeapon && isThisRangeWeapon)
            {
              reloadHint.text = "Кропильница полна!";
            }
        }
        else
        {
            reloadHint.text = null;
        }
    }

   private void UpdateReloadingTimer()
    {
        if(rangeWeapon.IsReloading)
        {
            reloadHint.text = null;
            ReloadTimer();
        }
    }

    private void ReloadTimer()
    {
        currentReloadingTime -= Time.deltaTime;
        reloadImageTimer.fillAmount = currentReloadingTime / maxReloadingTime;

        if(currentReloadingTime <= 0 || !rangeWeapon.IsReloading)
        {
            currentReloadingTime = maxReloadingTime;
        }
    }



    private void OnEnable()
    {
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged += UpdateUIAmmo;
        PlayerInformation.GetInstance().PlayerController.OnChangeWeapon += PlayerController_OnChangeWeapon;
    }


    private void OnDisable()
    {
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged -= UpdateUIAmmo;
        PlayerInformation.GetInstance().PlayerController.OnChangeWeapon -= PlayerController_OnChangeWeapon;
    }

    private void PlayerController_OnChangeWeapon(PlayerWeaponChanger.WeaponSpellsHolder obj)
    {
        isThisMeleeWeapon = obj.Weapon1.WeaponType == WeaponType.Mili;
        isThisRangeWeapon = obj.Weapon1.WeaponType == WeaponType.Range;

       if(isThisMeleeWeapon)
        {
            ammoAmout.enabled = false;
        }

       if(isThisRangeWeapon)
        {
            ammoAmout.enabled = true;
            ammoAmout.text = waterValue.ToString();
        }
    }

    private void UpdateUIAmmo(DamagebleParam.ParamType paramType, float value, float maxValue)
    {

      if(paramType == DamagebleParam.ParamType.HolyWater)
        {
            waterValue = value;
            ammoAmout.text = value.ToString();
        }
    }

   

}
