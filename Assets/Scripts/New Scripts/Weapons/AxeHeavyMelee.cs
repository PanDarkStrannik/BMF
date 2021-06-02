using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AxeHeavyMelee : AWeapon, IDamagingWeapon
{
    [SerializeField] private float toAttackTime = 1f;
    [SerializeField] private float damagingTime = 0.3f;
    [SerializeField] private float reloadAttack = 0.7f;
    [SerializeReference] private DamageArea damageArea;

    public override WeaponType WeaponType => WeaponType.Special;

    public void Attack()
    {
        if(state == WeaponState.Serenity && this.gameObject.activeSelf)
        {
          StopAllCoroutines();
          StartCoroutine(Charge(0));

        }
    }

    public override void UseWeapon()
    {
        Attack();
    }

    protected override IEnumerator Charge(float time)
    {
        if(state != WeaponState.Charge)
        {
           while(true)
           {
                if(isWeaponCharged)
                {
                    State = WeaponState.Charge;
                    Debug.Log("Charge");
                }
                else
                {
                    StartCoroutine(Damaging(damagingTime));
                    Debug.Log("Release");
                   
                }
                yield return new WaitForEndOfFrame();
           }
        }
       
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

}
