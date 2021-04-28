using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class ADamageble : MonoBehaviour, IDamageble
{
    [SerializeField] protected DamagebleParamDatas datas;
    [SerializeField] private LayerMask layer;

    public delegate void OnDamagedHelper();
    public event OnDamagedHelper OnDamaged;
    public UnityEvent Damaged;
    public event OnDamagedHelper OnHeal;

    public delegate void OnChangedParamHelper(float damageValue, ADamageble damageblePlace);
    public event OnChangedParamHelper OnDamagedWithValue;
    public event OnChangedParamHelper OnHealWithValue;

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
        set
        {
            layer = value;
        }
    }

    public abstract void ApplyDamage(DamageByType weapon);

    public abstract void ApplyHeal(DamageByType healWeapon);

    public virtual void ChangeParam(DamagebleParam.ParamType type, float value)
    {
        var temp = datas.FindAllByParamType(type);

        if (value == 0)
        {
            return;
        }
        else if (value > 0)
        {
            foreach(var e in temp)
            {
                e.Enlarge(value);
            }
        }
        else if (value < 0)
        {
            foreach(var e in temp)
            {
                e.ApplyDamage(value);
            }
        }        
    }

    protected void DamageEvent()
    {
        OnDamaged?.Invoke();
        Damaged?.Invoke();
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
