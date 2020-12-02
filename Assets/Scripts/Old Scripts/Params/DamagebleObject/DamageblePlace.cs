using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageblePlace : ADamageble
{

    public override void ApplyDamage(DamageByType weapon)
    {
        var allWeak = datas.FindAllByWeak(weapon.DamageType);

        if (allWeak != null)
        {

            foreach (var weak in allWeak)
            {
                float damage = weapon.DamageValue;
                foreach (var strongData in weak.Strongs)
                {
                    if (strongData.DamageType == weapon.DamageType)
                    {
                        damage -= strongData.DamageValue;
                    }
                }
                foreach(var weakData in weak.Weakneses)
                {
                    if(weakData.DamageType == weapon.DamageType)
                    {
                        damage += weakData.DamageValue;
                    }
                }
                if (damage < 0)
                {
                    damage = 0;
                }
                weak.ApplyDamage(damage);

                DamageEventWithValue(damage, this);
            }
            DamageEvent();
        }
    }

    public override void ApplyHeal(DamageByType healWeapon)
    {
        var allWeak = datas.FindAllByWeak(healWeapon.DamageType);

        if (allWeak != null)
        {

            foreach (var weak in allWeak)
            {
                float damage = healWeapon.DamageValue;
                foreach (var strongData in weak.Strongs)
                {
                    if (strongData.DamageType == healWeapon.DamageType)
                    {
                        damage += strongData.DamageValue;
                    }
                }
                if (damage < 0)
                {
                    damage = 0;
                }
                weak.Enlarge(damage);

                HealEventWithValue(damage, this);
            }
            HealEvent();
        }
    }
}
