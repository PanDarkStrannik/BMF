using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AxeHeavyMelee : AWeapon, IDamagingWeapon
{                                              
    [SerializeField] private float toAttackTime;
    [SerializeField] private float damagingTime;
    [SerializeField] private float reloadAttack;
    [SerializeReference] private DamageArea damageArea;

    [SerializeField] private float chargeTime = 3f;
    [SerializeField] private float chargeDamageBonus;

    private float startChargeTime;

    public override WeaponType WeaponType => WeaponType.Special;


    private void OnDisable()
    {
        StopAllCoroutines();
        state = WeaponState.Serenity;
    }

    public void Attack()
    {
        if(state == WeaponState.Serenity && this.gameObject.activeSelf)
        {
          StopAllCoroutines();
          StartCoroutine(Charge());

        }
    }

    public override void UseWeapon()
    {
        Attack();
    }

    protected override IEnumerator Charge()
    {
        State = WeaponState.Charge;
        if(state == WeaponState.Charge)
        {
            //while(startChargeTime < chargeTime)
            //{
                //startChargeTime += Time.deltaTime;
                //Debug.Log(startChargeTime);
            //}

           yield return new WaitWhile(()=> IsWeaponCharged);
            startChargeTime = 0;
           StartCoroutine(Damaging(damagingTime));
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
          yield return new WaitForSecondsRealtime(time);
          State = WeaponState.Serenity;
    }

}
