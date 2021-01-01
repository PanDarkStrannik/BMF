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
    [SerializeField] protected int attackValues = 3;
    [Min(1)]
    [SerializeField] protected int bulletsOnAttack = 1;
    [SerializeField] private Spread spread;


    public override WeaponType WeaponType
    {
        get
        {
            return WeaponType.Range;
        }
    }


    protected int attackCount = 0;

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
        if (state != WeaponState.ImposibleAttack && attackCount >= attackValues)
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
            for (int i = 0; i < bulletsOnAttack; i++)
            {
                bulletSpawner.SpawnFirstObjectInQueue(gunPosition.position, spread.SpreadAngle(gunPosition.rotation));
            }
            attackCount++;
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
            attackCount = 0;
            StartCoroutine(Serenity(0f));
        }
    }

    protected override IEnumerator Serenity(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        State = WeaponState.Serenity;
        StopAllCoroutines();
    }

    [System.Serializable]
    private class Spread
    {
        [Range(0, 90)]
        [SerializeField] private int coneAngle = 0;
        [SerializeField] private bool spreadInY = false;
        [SerializeField] private bool spreadInX = false;

        public Quaternion SpreadAngle(Quaternion currentAngle)
        {
            var randX = Random.Range(-1f, 1f);
            var randY = Random.Range(-1f, 1f);
            if (!spreadInX)
            {
                randX = 0;
            }
            if (!spreadInY)
            {
                randY = 0;
            }
            var coneRandomAngle = new Vector3(currentAngle.eulerAngles.x + coneAngle * randX,
               currentAngle.eulerAngles.y + coneAngle * randY,
               currentAngle.eulerAngles.z);

            var spreadAngle = Quaternion.Euler(coneRandomAngle);
            return spreadAngle;
        }
    }

    //[System.Serializable]
    //private class AttackParametres
    //{
    //    [Range(0, 90)]
    //    [SerializeField] private int coneAngle = 0;
    //    [SerializeField] private bool spreadInY = false;
    //    [SerializeField] private bool spreadInX = false;

        
    //}
}
