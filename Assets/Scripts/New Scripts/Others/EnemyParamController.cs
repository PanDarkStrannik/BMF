﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyParamController : ParamController
{
    [SerializeField] private UnityEvent<AttackAI.AttackStopVariants, float> damageEvent;
    [SerializeField] private float damageToStopAttack = 1f;
    [SerializeField] private float timeToStopAttack = 1f;
    [SerializeField] protected int pointsForKill = 1;
    [SerializeReference] private SpawnedObject spawnedObject;   

    //public delegate void OnEnemyDieEventHelper();
    //public event OnEnemyDieEventHelper OnEnemyDie;
    public UnityEvent OnEnemyDie;


    protected override void CheckTypeAndValues(DamagebleParam.ParamType type, float value, float maxValue)
    {
        switch (type)
        {
            case DamagebleParam.ParamType.Health:
                Debug.Log($"{type} + {value} + {maxValue}");
                if(value >= damageToStopAttack)
                {
                    damageEvent?.Invoke(AttackAI.AttackStopVariants.StopOnDamage, timeToStopAttack);
                }
                break;
        }
    }

    protected override IEnumerator NullHealth()
    {
        foreach(var e in deactiveScripts)
        {
            e.enabled = false;
        }
        OnEnemyDie?.Invoke();
        yield return new WaitForSeconds(timeToDeactive);
        PointCounter.Instance.AddPoints(pointsForKill);
        spawnedObject.Die();
        paramSum.SetDefault();
    }

}
