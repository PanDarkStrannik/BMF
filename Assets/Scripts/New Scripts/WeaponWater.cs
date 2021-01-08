using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWater : WeaponRange
{
    [SerializeField] private ParamController.ResourcesUser resourcesUser;


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

    public void Reload()
    {
        StopAllCoroutines();
        StartCoroutine(Reload(attackParametres.ReloadTime));
    }


}
