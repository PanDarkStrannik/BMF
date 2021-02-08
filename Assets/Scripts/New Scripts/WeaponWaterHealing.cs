using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWaterHealing : WeaponHealing
{
    [SerializeField] private ParamController.ResourcesUser resourcesUser;

    public override void Heal(GameObject healingObject)
    {
        this.healingObject = healingObject;
        if (resourcesUser.ParamController == null)
        {
            if (state != WeaponState.ImposibleAttack && currentHealCount >= waterHealingData.HealCount)
            {
                StopCoroutine(Damaging(waterHealingData.AttackTime));
                StartCoroutine(Reload(waterHealingData.ReloadTime));
            }
            else if (state == WeaponState.Serenity)
            {
                currentHealCount++;
                StartCoroutine(Damaging(waterHealingData.AttackTime));
            }
        }
        else
        {
            if(state == WeaponState.Serenity)
            {
                if(resourcesUser.TryUseResource())
                {
                    StartCoroutine(Damaging(waterHealingData.AttackTime));
                }
            }
        }
    }

    protected override IEnumerator Damaging(float time)
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

            yield return new WaitForSecondsRealtime(time);
            StartCoroutine(Serenity(0f));
        }
    }
}
