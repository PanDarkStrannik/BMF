using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADamageble : MonoBehaviour, IDamageble
{
    [SerializeField] protected DamagebleParamDatas datas;
    [SerializeField] private LayerMask layer;

    public delegate void OnDamagedHelper();
    public event OnDamagedHelper OnDamaged;
    public event OnDamagedHelper OnHeal;

    public delegate void OnDamagedValueHelper(float damageValue, ADamageble damageblePlace);
    public event OnDamagedValueHelper OnDamagedWithValue;
    public event OnDamagedValueHelper OnHealWithValue;

    public delegate void OnPushHelper(Vector3 push, ForceMode forceMode);
    public event OnPushHelper PushEvent;

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

    public abstract void ApplyHeal(DamageByType healWeapon);

    protected void DamageEvent()
    {
        OnDamaged?.Invoke();
    }

    protected void DamageEventWithValue(float value, ADamageble damageblePlace)
    {
        OnDamagedWithValue?.Invoke(value, damageblePlace);
    }

    protected void HealEvent()
    {
        OnHeal?.Invoke();
    }

    protected void HealEventWithValue(float value, ADamageble damageblePlace)
    {
        OnHealWithValue?.Invoke(value, damageblePlace);
    }

    public virtual void Push(Vector3 pushValue, ForceMode forceMode)
    {
        PushEvent?.Invoke(pushValue, forceMode);
    }

}
