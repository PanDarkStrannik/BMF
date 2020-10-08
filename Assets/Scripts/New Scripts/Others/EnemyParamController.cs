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

    public delegate void OnEnemyDieEventHelper();
    public event OnEnemyDieEventHelper OnEnemyDie;


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
        yield return new WaitForSeconds(timeToDeactive);
        PointCounter.GetPointCounter().AddPoints(pointsForKill);
        OnEnemyDie?.Invoke();
        paramSum.SetDefault();
    }

}
