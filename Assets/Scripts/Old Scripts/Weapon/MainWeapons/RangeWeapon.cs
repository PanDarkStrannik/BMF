using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : AWeapon
{
    [SerializeField] protected Spawner bulletSpawner;
    [SerializeField] protected Transform gunPosition;
    [SerializeField] protected float attackTime;
    [SerializeField] protected float toShootTime=0f;
    [SerializeField] protected float timeToReload = 0f;
    [SerializeField] protected int bulletsValue=3;

    protected int bulletsCount = 0;

    private void Awake()
    {
        bulletSpawner.CreateSpawner();
        GameEvents.onBulletDie+=bulletSpawner.ReturnObject;
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
        if (/*!isAttack && !isReload*/ state == WeaponState.Serenity)
        {
            state = WeaponState.Attack;
            StartCoroutine(Shoot());
        }
    }



    private IEnumerator Shoot()
    {
        BeforeShoot();
        yield return new WaitForSecondsRealtime(toShootTime);
        if (state==WeaponState.Attack)
        {
            InShoot();
            bulletSpawner.SpawnObject(gunPosition.position, gunPosition.rotation);
            yield return new WaitForSecondsRealtime(attackTime);
            state = WeaponState.Serenity;
        }
    }

    protected virtual void BeforeShoot()
    {
        BulletCounter();
        if (state == WeaponState.Attack)
        {
            events.OnAnimEvent(AnimationController.AnimationType.RangeAttack);
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
