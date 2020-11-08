using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : AWeapon
{
    [SerializeField] protected Spawner bulletSpawner;
    [SerializeField] protected Transform gunPosition;
    [SerializeField] private List<DamageByType> weaponData;
    [SerializeField] protected LayerMask layer;
    [SerializeField] protected float attackTime;
    [SerializeField] protected float toShootTime=0f;
    [SerializeField] protected float timeToReload = 0f;
    [SerializeField] protected int bulletsValue=3;


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
       // GameEvents.onBulletDie+=bulletSpawner.ReturnObject;
        foreach(var e in bulletSpawner.spawned_objects)
        {
            e.GetComponent<IBullet>().Init(weaponData,layer);
        }
    }



    private void OnDisable()
    {
        state = WeaponState.Serenity;
    }

    public override void Attack()
    {
        if (state == WeaponState.Serenity)
        {
            state = WeaponState.Attack;
            StartCoroutine(Shoot());
        }
        base.Attack();
    }



    private IEnumerator Shoot()
    {
        BeforeShoot();
        AttackStartEvent?.Invoke(true);
        yield return new WaitForSecondsRealtime(toShootTime);
        if (state==WeaponState.Attack)
        {
            InShoot();
            bulletSpawner.SpawnFirstObjectInQueue(gunPosition.position, gunPosition.rotation);
            yield return new WaitForSecondsRealtime(attackTime);
            AttackStartEvent?.Invoke(false);
            state = WeaponState.Serenity;
        }
    }

    protected virtual void BeforeShoot()
    {
        BulletCounter();
        if (state == WeaponState.Attack)
        {
            OnAttackEvent?.Invoke();
        }
    }

    protected virtual void InShoot()
    {
        
    }

    protected virtual void BulletCounter()
    {

        if (bulletsCount >= bulletsValue)
        {
            StopCoroutine(Shoot());
            StartCoroutine(Reload());
            return;
        }

        bulletsCount++;
    
    }

    protected virtual IEnumerator Reload()
    {
        state = WeaponState.Reload;
        yield return new WaitForSecondsRealtime(timeToReload);
        bulletsCount = 0;
        state = WeaponState.Serenity;
    }

}
