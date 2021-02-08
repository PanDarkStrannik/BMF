using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : AWeapon
{
    [SerializeField] protected Spawner bulletSpawner;
    [SerializeField] protected Transform gunPosition;
    [SerializeField] private List<DamageByType> weaponData;
    [SerializeField] protected LayerMask layer;
    [SerializeField] protected float toAttackTime;
    [SerializeField] protected float reloadTime = 0f;
    [SerializeField] protected int bulletsValue = 3;


    public override WeaponType WeaponType
    {
        get
        {
            return WeaponType.Range;
        }
    }


    protected int bulletsCount = 0;

    protected override void Awake()
    {
        base.Awake();
        bulletSpawner.CreateSpawner();
        foreach (var e in bulletSpawner.spawned_objects)
        {
            e.GetComponent<IBullet>().Init(weaponData, layer);
        }
    }



    private void OnDisable()
    {
        state = WeaponState.Serenity;
    }

    public override void Attack()
    {
        if (state != WeaponState.ImposibleAttack && bulletsCount >= bulletsValue)
        {
            StartCoroutine(Reload(reloadTime));
        }
        else if (state == WeaponState.Serenity)
        {
            StartCoroutine(Damaging(toAttackTime));
        }
    }



    protected override IEnumerator Damaging(float time)
    {
        State = WeaponState.Attack;
        if (state == WeaponState.Attack)
        {
            yield return new WaitForSecondsRealtime(time);
            bulletSpawner.SpawnFirstObjectInQueue(gunPosition.position, gunPosition.rotation);
            bulletsCount++;
            StartCoroutine(Serenity(0f));
        }
    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        if (state == WeaponState.Reload)
        {
            StopCoroutine(Damaging(toAttackTime));
            yield return new WaitForSecondsRealtime(time);
            bulletsCount = 0;
            StartCoroutine(Serenity(0f));
        }
    }

    protected override IEnumerator Serenity(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        State = WeaponState.Serenity;
        StopAllCoroutines();
    }


   
}
