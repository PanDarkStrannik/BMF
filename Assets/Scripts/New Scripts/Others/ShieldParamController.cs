using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldParamController : ParamController
{
    [SerializeField] private UnityEvent OnNullHealth;

    protected override void CheckTypeAndValues(DamagebleParam.ParamType type, float value, float maxValue)
    {
        switch (type)
        {
            case DamagebleParam.ParamType.Health:
                if(value <= 0)
                {
                    StartCoroutine(NullHealth());
                }
                break;
        }
    }

    protected override IEnumerator NullHealth()
    {
        yield return new WaitForSeconds(1);
        OnNullHealth?.Invoke();
        paramSum.SetDefault();
    }
}
