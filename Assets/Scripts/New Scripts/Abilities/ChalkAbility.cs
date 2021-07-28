﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChalkAbility : ActiveAbility
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private SpawnedObject spawnShield;

    public static event Action<SpawnedObject> OnShieldGet;

    protected override void Awake()
    {
        base.Awake();
        spawner.CreateSpawner();
        foreach (var s in spawner.spawned_objects)
        {
            spawnShield = s.GetComponent<SpawnedObject>();
            if(spawnShield != null)
            {
                OnShieldGet?.Invoke(spawnShield);
            }
        }
    }

    private void OnEnable()
    {
        ShieldParamController.OnShieldDestroyed += ShieldDestroyedByDamage;
    }


    private void OnDisable()
    {
        ShieldParamController.OnShieldDestroyed -= ShieldDestroyedByDamage;
    }


    public override void UseAbility()
    {
        if(abilityState == AbilityState.Enabled)
        {
            StartCoroutine(StartUse(abilityParams.TimeForUse));
        }
    }

    protected override IEnumerator StartUse(float time)
    {
        AbilityState = AbilityState.Enabled;
        if(abilityState == AbilityState.Enabled)
        {
            yield return new WaitForSeconds(time);
            var melPos = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
            spawner.SpawnFirstObjectInQueue(melPos, Quaternion.identity);
            StartCoroutine(Using(abilityParams.ActiveTime));
        }
    }

    protected override IEnumerator Using(float time)
    {
        AbilityState = AbilityState.Using;
        if(abilityState == AbilityState.Using)
        {
            //check if shield is dead already
            yield return new WaitForSeconds(time);
            spawnShield.Die();
            StartCoroutine(CoolDown(abilityParams.CoolDownTime));
        }
    }

    protected override IEnumerator CoolDown(float time)
    {
        if(abilityState != AbilityState.Disabled)
        {
           AbilityState = AbilityState.Disabled;
            yield return new WaitForSeconds(time);
            StopAllCoroutines();
            AbilityState = AbilityState.Enabled;
        }
    }

    private void ShieldDestroyedByDamage()
    {
        StopAllCoroutines();
        StartCoroutine(CoolDown(1));
    }

    
}
