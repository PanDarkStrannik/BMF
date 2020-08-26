using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : AWeapon
{
    [SerializeField] protected Spawner bulletSpawner;
    [SerializeField] protected Transform gunPosition;
    [SerializeField] protected float attackTime;
    [SerializeField] protected float toShootTime=0f;



    private void Awake()
    {
        bulletSpawner.CreateSpawner();
        GameEvents.onBulletDie+=bulletSpawner.ReturnObject;
        foreach(var e in bulletSpawner.spawned_objects)
        {
            e.GetComponent<IBullet>().Init(weaponData,layer);
        }
    }


    public override void Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            StartCoroutine(Shoot());
        }
    }



    private IEnumerator Shoot()
    {
        BeforeShoot();
        yield return new WaitForSeconds(toShootTime);
        if (isAttack)
        {
            InShoot();
            bulletSpawner.SpawnObject(gunPosition.position, gunPosition.rotation);
        }
        yield return new WaitForSeconds(attackTime);
        isAttack = false;
    }

    protected virtual void BeforeShoot()
    {
        events.OnAnimEvent(AnimationController.AnimationType.RangeAttack);
    }

    protected virtual void InShoot()
    {

    }
}
