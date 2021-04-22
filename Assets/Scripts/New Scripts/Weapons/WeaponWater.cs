using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponWater : WeaponRange
{
    [SerializeField] private ParamController.ResourcesUser resourcesUser;

    private float waterCharges;
    private float maxWaterCharges;

    public static event Action OnDisturbReload;
   
    private void Start()
    {
        waterCharges = resourcesUser.ParamController.DamagebleParams.typesValues[DamagebleParam.ParamType.HolyWater];
        maxWaterCharges = resourcesUser.ParamController.DamagebleParams.typesMaxValues[DamagebleParam.ParamType.HolyWater];

        resourcesUser.ParamController.DamagebleParams.OnParamChanged += UpdateAmmo;
    }

    //private void PlayerController_OnChangeWeapon(PlayerWeaponChanger.WeaponSpellsHolder obj)
    //{
    //    if(isReloading)
    //    {
    //      DisturbReloading();
    //    }
    //}

    //private void PlayerController_OnPlayerMoved(Vector3 obj)
    //{
    //   if(isReloading && obj != Vector3.zero)
    //    {
    //      DisturbReloading();
    //    }
    //}

    protected override void OnDestroy()
    {
        resourcesUser.ParamController.DamagebleParams.OnParamChanged -= UpdateAmmo;
    }



    public override void Attack()
    {
        if (resourcesUser.ParamController == null)
        {
            if (state != WeaponState.ImposibleAttack && attackCount >= attackParametres.AttackValues)
            {
                StartCoroutine(Reload(attackParametres.ReloadTime));
            }
            else if (state == WeaponState.Serenity)
            {
                StartCoroutine(Damaging(attackParametres.ToAttackTime));
            }
        }
        else
        {
            if (state == WeaponState.Serenity)
            {
                if (resourcesUser.TryUseResource())
                {
                    StartCoroutine(Damaging(attackParametres.ToAttackTime));
                }
            }
        }
    }

    //public void Reload()
    //{
    //    if(waterCharges >= maxWaterCharges)
    //    {
    //        Debug.Log("Перезарядка не требуется");
    //        return;
    //    }
    //    else
    //    {
    //        StopAllCoroutines();
    //        Debug.Log("Перезаряжаем...");
    //        StartCoroutine(Reload(attackParametres.ReloadTime));
    //    }
    //}

    //private void DisturbReloading()
    //{
    //    StopAllCoroutines();
    //    isReloading = false;
    //    OnDisturbReload?.Invoke();

    //    if (!this.gameObject.activeSelf)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        StartCoroutine(Serenity(0f));
    //    }
       
    //}

    public void UpdateAmmo(DamagebleParam.ParamType paramType, float value, float maxValue)
    {

        if (paramType == DamagebleParam.ParamType.HolyWater)
        {
            waterCharges = value;
            maxWaterCharges = maxValue;
        }
    }





}
