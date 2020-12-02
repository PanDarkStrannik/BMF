using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiliWeapon : AWeapon
{
    [SerializeField] private float timeWeaponColider=1f;
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
        if (state==AWeapon.WeaponState.Serenity)
        {
            state = AWeapon.WeaponState.Attack;
            StartCoroutine(ChangeColider());    
        }
        base.Attack();
    }

    private IEnumerator ChangeColider()
    {
        AttackStartEvent?.Invoke(true);
        yield return new WaitForSecondsRealtime(timeToWeaponAttack);
        if (state==AWeapon.WeaponState.Attack)
        {
            OnAttackEvent?.Invoke();

            damageArea.gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(timeWeaponColider);
            damageArea.gameObject.SetActive(false);
            state = AWeapon.WeaponState.Serenity;
        }
        AttackStartEvent?.Invoke(false);
    }


    private void OnDisable()
    {
        state = AWeapon.WeaponState.Serenity;
    }

}
