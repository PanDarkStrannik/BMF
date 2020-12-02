using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : AWeapon
{

    [SerializeReference] private DamageArea damageArea;

    public override WeaponType WeaponType
    {
        get
        {
            return WeaponType.Mili;
        }
    }

    protected override IEnumerator Damaging(float time)
    {
        if (state == AWeapon.WeaponState.Attack)
        {
            damageArea.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(time);
        damageArea.gameObject.SetActive(false);
        state = AWeapon.WeaponState.Serenity;
    }


}
