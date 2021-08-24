using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCrossWeapon : AWeapon, IDamagingWeapon//, IMoonLigthUser
{
    [SerializeField] private DamageArea _damageArea;
    [SerializeField] private StanArea _stanArea;
    [SerializeField,Min(0f)] private float _attackDuration;
    [SerializeField, Min(0f)] private float _reloadTime;

    public event Action OnMoonLigthUsed;

    public override WeaponType WeaponType => WeaponType.Directional;

    private void Start()
    {
        //State = WeaponState.ImposibleAttack;
        State = WeaponState.Serenity;
        _damageArea.gameObject.SetActive(false);
        _stanArea.gameObject.SetActive(false);
    }


    public override void UseWeapon()
    {
        Attack();
    }

    public void Attack()
    {
        if (State == WeaponState.Serenity)
        {
            StartCoroutine(Attacking(_attackDuration));
        }
    }

    protected override IEnumerator Attacking(float time)
    {
        _damageArea.gameObject.SetActive(true);
        _stanArea.gameObject.SetActive(true);
        yield return base.Attacking(time);
        _damageArea.gameObject.SetActive(false);
        _stanArea.gameObject.SetActive(false);
        OnMoonLigthUsed?.Invoke();
        yield return StartCoroutine(Reload(_reloadTime));
        yield return StartCoroutine(Serenity(0f));
    }

    //public void OnMoonLigthEnter()
    //{
    //    State = WeaponState.Serenity;
    //}

    //public void OnMoonLigthExit()
    //{
    //    StopAllCoroutines();
    //    State = WeaponState.ImposibleAttack;
    //}
}
