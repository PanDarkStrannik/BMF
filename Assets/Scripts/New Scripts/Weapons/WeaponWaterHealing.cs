using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWaterHealing : WeaponHealing
{
    [SerializeField] private ParamController.ResourcesUser resourcesUser;

    public override void Heal(GameObject healingObject)
    {
        this.healingObject = healingObject;

        if(this.gameObject.activeSelf)
        {
               if (resourcesUser.ParamController == null)
               {
                   if (state != WeaponState.ImposibleAttack && currentHealCount >= waterHealingData.HealCount)
                   {
                       StopCoroutine(Attacking(waterHealingData.TimeBetweeenUse));
                       StartCoroutine(Reload(waterHealingData.ReloadTime));
                   }
                   else if (state == WeaponState.Serenity)
                   {
                      currentHealCount++;
                      StartCoroutine(Attacking(waterHealingData.TimeBetweeenUse));
                   }
               }
               else
               {
                   if(state == WeaponState.Serenity)
                   {
                       if(resourcesUser.TryUseResource())
                       {
                            StartCoroutine(Attacking(waterHealingData.TimeBetweeenUse));
                       }
                   }
               }
        }
    }

    protected override IEnumerator Attacking(float time)
    {
        State = WeaponState.Attack;
        if (state == WeaponState.Attack)
        {
            if (resourcesUser.ParamController == null)
            {
                if (FindComponentInIerarhy<ParamController>(healingObject, out ParamController finded))
                {
                    foreach (var healType in waterHealingData.WeaponData)
                    {
                        finded.DamagebleParams.HealAllByType(healType);
                    }
                }
            }
            else
            {
                foreach (var healType in waterHealingData.WeaponData)
                {
                    resourcesUser.ParamController.DamagebleParams.HealAllByType(healType);
                }
            }

            yield return new WaitWhile(() => IsWeaponCharged);
            StartCoroutine(Serenity(0f));
        }
    }
}
