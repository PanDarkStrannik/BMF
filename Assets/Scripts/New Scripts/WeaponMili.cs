using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMili : AWeapon
{
    [SerializeField] private float damagingTime = 1f;
    [SerializeField] private float reloadTime = 0.3f;
    [SerializeField] private float timeToWeaponAttack = 0f;
    [SerializeReference] private DamageArea damageArea;

    public override WeaponType WeaponType
    {
        get
        {
            return WeaponType.Mili;
        }
    }


    public override void Attack()
    {
        if (state == AWeapon.WeaponState.Serenity)
        {
            StartCoroutine(Damaging(damagingTime));
        }        
    }

    protected override IEnumerator Damaging(float time)
    {
        State = WeaponState.Attack;
        if (state == WeaponState.Attack)
        {
            damageArea.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(time);
            damageArea.gameObject.SetActive(false);
            StartCoroutine(Serenity(reloadTime));
        }
    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        if (state == WeaponState.Reload)
        {
            yield return new WaitForSecondsRealtime(time);
            StartCoroutine(Serenity(0f));
        }
    }

    protected override IEnumerator Serenity(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        State = WeaponState.Serenity;
    }


    private void OnDisable()
    {
        StopAllCoroutines();
        state = AWeapon.WeaponState.Serenity;
    }
}
