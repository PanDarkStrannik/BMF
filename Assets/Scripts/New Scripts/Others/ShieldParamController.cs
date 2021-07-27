using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ShieldParamController : ParamController
{
    [SerializeField] private SpawnedObject spawnedObj;
    [SerializeField] private List<ShieldNullEvent> shieldNullEvents;

    public static event Action OnShieldDestroyed;
    
    protected override IEnumerator NullHealth()
    {
        yield return new WaitForSeconds(timeToDeactive);
        InvokeNullEvents();
        OnShieldDestroyed?.Invoke();
        spawnedObj.Die();
        paramSum.SetDefault();
    }

    private void InvokeNullEvents()
    {
        if(shieldNullEvents.Count > 0)
        {
            foreach (var s in shieldNullEvents)
            {
                s.Invoke();
            }
        }
    }
}

[Serializable]
public class ShieldNullEvent
{
    [SerializeField] private float timeToInvoke;
    [SerializeField] private UnityEvent OnNullHealth;

    public IEnumerator Invoke()
    {
        yield return new WaitForSeconds(timeToInvoke);
        OnNullHealth?.Invoke();
    }
}