using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWater : WeaponRange
{
    public override void Attack()
    {
        if (state == WeaponState.Serenity)
        {
            StartCoroutine(Damaging(toAttackTime));
        }
    }

    public void Reload()
    {
        StopAllCoroutines();
        StartCoroutine(Reload(reloadTime));
    }

}
