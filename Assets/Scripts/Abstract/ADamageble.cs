using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADamageble : MonoBehaviour, IDamageble
{
    [SerializeField] protected DamagebleParamDatas datas;
    [SerializeField] private LayerMask layer;

    public delegate void OnDamagedHelper();
    public event OnDamagedHelper OnDamaged;

    public DamagebleParamDatas Datas
    {
        get
        {

            return datas;

        }
    }

    public LayerMask Layer
    {
        get
        {
            return layer;
        }
    }

    public abstract void ApplyDamage(DamageByType weapon);

    protected void DamageEvent()
    {
        OnDamaged?.Invoke();
    }
}
