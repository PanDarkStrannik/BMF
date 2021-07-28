using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ShieldParamController : ParamController
{
    [SerializeField] private SpawnedObject spawnedObj;
    [SerializeField] private AudioSource destroySound;
    

    public static event Action OnShieldDestroyed;
    
    protected override IEnumerator NullHealth()
    {
        destroySound.Play();
        yield return new WaitForSeconds(timeToDeactive);
        OnShieldDestroyed?.Invoke();
        spawnedObj.Die();
        paramSum.SetDefault();
    }

    
}

