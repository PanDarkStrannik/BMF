using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Text ammoAmout;
    [SerializeField] private WeaponRange rangeWeapon;
    [SerializeField] private Image reloadImage;
    

    private float waterValue = 0f;
    public float reloadingTime;

    private void Start()
    {
        waterValue = PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.typesValues[DamagebleParam.ParamType.HolyWater];
        reloadingTime = rangeWeapon.attackParametres.ReloadTime;
         
    }

    private void Update()
    {
        if(reloadImage.IsActive())
        {
            Debug.Log("time--");
            reloadImage.fillAmount = reloadingTime / reloadingTime;
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
