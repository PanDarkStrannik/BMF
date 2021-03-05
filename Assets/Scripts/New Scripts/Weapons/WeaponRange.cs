using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : AWeapon, IDamagingWeapon
{
    [SerializeField] protected Spawner bulletSpawner;
    [SerializeField] protected Transform gunPosition;
    //[SerializeField] private List<DamageByType> weaponData;
    //[SerializeField] protected LayerMask layer;
    //[SerializeField] protected float toAttackTime;
    //[SerializeField] protected float reloadTime = 0f;
    //[SerializeField] protected int attackValues = 3;
    //[Min(1)]
    //[SerializeField] protected int bulletsOnAttack = 1;
    [SerializeField] public AttackParametres attackParametres;
    [SerializeField] protected Spread spread;
      private ParamController weaponParam;


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
        weaponParam = FindObjectOfType<ParamController>();
        base.Awake();
        bulletSpawner.CreateSpawner();
        foreach (var e in bulletSpawner.spawned_objects)
        {
            e.GetComponent<IBullet>().Init(attackParametres.WeaponData, attackParametres.Layer);
        }
    }



    private void OnDisable()
    {
        state = WeaponState.Serenity;
    }

    public virtual void Attack()
    {
        if (state != WeaponState.ImposibleAttack && attackCount >= attackParametres.AttackValues)
        {
            StartCoroutine(Reload(attackParametres.ReloadTime));
        }
        else if (state == WeaponState.Serenity)
        {
            StartCoroutine(Damaging(attackParametres.ToAttackTime));
        }
    }

    public override void UseWeapon()
    {
        Attack();
    }



    protected override IEnumerator Damaging(float time)
    {
        State = WeaponState.Attack;
        if (state == WeaponState.Attack)
        {
            yield return new WaitForSecondsRealtime(time);
            for (int i = 0; i < attackParametres.BulletsOnAttack; i++)
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
            StopCoroutine(Damaging(attackParametres.ToAttackTime));
            yield return new WaitForSecondsRealtime(time);
            if(WeaponType == WeaponType.Range)
            {
                if(TryReturnNeededWeaponType<WeaponWater>(out WeaponWater water))
                {
                    Debug.Log("Должна быть перезарядка через 5 сек");
                    weaponParam.DamagebleParams.ChangeParam(DamagebleParam.ParamType.HolyWater, 10);
                }
            }
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
    protected class Spread
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


    [System.Serializable]
    public class AttackParametres
    {
        [SerializeField] private List<DamageByType> weaponData;
        [SerializeField] private LayerMask layer;
        [Min(0)]
        [SerializeField] private float toAttackTime;
        [Min(0)]
        [SerializeField] private float reloadTime = 0f;
        [Min(0)]
        [SerializeField] private int attackValues = 3;
        [Min(1)]
        [SerializeField] private int bulletsOnAttack = 1;

        public List<DamageByType> WeaponData
        {
            get
            {
                return weaponData;
            }
        }
        public LayerMask Layer
        {
            get
            {
                return layer;
            }
        }

        public float ToAttackTime
        {
            get
            {
                return toAttackTime;
            }
        }

        public float ReloadTime
        {
            get
            {
                return reloadTime;
            }
        }

        public int AttackValues
        {
            get
            {
                return attackValues;
            }
        }

        public int BulletsOnAttack
        {
            get
            {
                return bulletsOnAttack;
            }
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
