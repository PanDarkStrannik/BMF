using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveAbility : AAbility
{
    [SerializeField] protected ParamController paramController;
    [SerializeField] protected ActiveSkillParams activeSkillParams;
    [SerializeField] protected ActiveSkillEvent activeSkillEvent;

    protected override AbilityType AbilityType => AbilityType.ActiveSkill;

    public override void UseAbility()
    {
        if(AbilityState == AbilityState.Enabled)
        {
            Debug.Log("Ability use start");
            StopAllCoroutines();
            StartCoroutine(StartUse(activeSkillParams.TimeForUse));
        }
    }


    protected override IEnumerator StartUse(float time)
    {
        AbilityState = AbilityState.Enabled;
        if(AbilityState == AbilityState.Enabled)
        {
            yield return new WaitForSecondsRealtime(time); // time for Start
            activeSkillEvent.InvokeAbilityEvent(AbilityState); // start event
            StartCoroutine(Using(activeSkillParams.ActiveTime));
        }

    }

    protected override IEnumerator Using(float time)
    {
        AbilityState = AbilityState.Using;
        if(AbilityState == AbilityState.Using)
        {
            yield return new WaitForSecondsRealtime(time); //active time
            activeSkillEvent.InvokeAbilityEvent(AbilityState);
            StartCoroutine(CoolDown(activeSkillParams.CoolDownTime));
        }
    }

    protected override IEnumerator CoolDown(float time)
    {
       if(AbilityState != AbilityState.Disabled)
       {
            AbilityState = AbilityState.Disabled;
            yield return new WaitForSecondsRealtime(time); // coolDown time
            StopAllCoroutines();
            AbilityState = AbilityState.Enabled;
       }
    }

}

[System.Serializable]
public class ActiveSkillParams
{
    [SerializeField, Min(0)] private float timeForUse;
    [SerializeField, Min(0)] private float activeTime;
    [SerializeField, Min(0)] private float coolDownTime;

    #region PROPERTIES
    public float TimeForUse { get => timeForUse; }
    public float ActiveTime { get => activeTime; }
    public float CoolDownTime { get => coolDownTime; }

    #endregion
}

[System.Serializable]
public class ActiveSkillEvent
{
   [SerializeField] private UnityEvent EventOnEnable;
   [SerializeField] private UnityEvent EventOnDisable;

    public void InvokeAbilityEvent(AbilityState state)
    {
        switch(state)
        {
            case AbilityState.Enabled:
                EventOnEnable?.Invoke();
                break;
            case AbilityState.Using:
                EventOnDisable?.Invoke();
                break;
        }
    }

}