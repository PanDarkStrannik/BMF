using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMili : AWeapon, IDamagingWeapon
{
    [SerializeField] private float toAttackTime = 1f;
    [SerializeField] private float damagingTime = 0.3f;
    [SerializeField] private float reloadAttack = 0.7f;
    [SerializeReference] private DamageArea damageArea;
    [SerializeField] private WeaponType weaponType;

    public override WeaponType WeaponType
    {
        get
        {
            return weaponType;
        }
    }


    public void Attack()
    {
        if (state == AWeapon.WeaponState.Serenity)
        {
            StopAllCoroutines();
            StartCoroutine(Damaging(toAttackTime));
        }        
    }

    public override void UseWeapon()
    {
        Attack();
    }

    protected override IEnumerator Damaging(float time)
    {
        State = WeaponState.Attack;
        if (state == WeaponState.Attack)
        {
            yield return new WaitForSecondsRealtime(time);
            damageArea.gameObject.SetActive(true);
            StartCoroutine(Reload(damagingTime));
        }
    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        if (state == WeaponState.Reload)
        {
            yield return new WaitForSecondsRealtime(time);
            damageArea.gameObject.SetActive(false);
            StartCoroutine(Serenity(reloadAttack));
        }
    }

    protected override IEnumerator Serenity(float time)
    {
        if (state != WeaponState.Serenity)
        {
            yield return new WaitForSecondsRealtime(time);
            State = WeaponState.Serenity;
            StopAllCoroutines();
        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
        state = AWeapon.WeaponState.Serenity;
    }
}
