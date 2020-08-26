using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParamController : ParamController
{
    [SerializeField] protected string bossName;
    [SerializeField] protected Sprite bossIcon;
    [SerializeField] protected BossUI bossUI;
    [SerializeField] private MainEvents events;

    private bool isFirstCheck = true;
    private bool isDeadYet = false;

    protected override void CheckTypeAndValues(DamagebleParam.ParamType type, float value, float maxValue)
    {
        switch(type)
        {
            case DamagebleParam.ParamType.Health:
                if (isFirstCheck)
                {
                    bossUI.InitializeBossView(bossName, bossIcon, maxValue);
                    isFirstCheck = false;
                }
                bossUI.ViewHealth(value);
                if (value <= 0 && !isDeadYet)
                {
                    events.OnAnimEvent(AnimationController.AnimationType.Death);
                    isDeadYet = true;
                }
                break;
        }
    }

    
}
