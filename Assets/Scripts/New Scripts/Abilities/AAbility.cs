using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum AbilityType 
{ 
    ActiveSkill, PassiveSkill
}

public enum AbilityState
{
    Enabled, Using, Disabled
}

public abstract class AAbility : MonoBehaviour
{

    [SerializeField] protected AbilityState abilityState = AbilityState.Enabled;
    [SerializeField] protected AbilityParams abilityParams;
    [SerializeField] protected List<AbilityEvent> abilityEvent = new List<AbilityEvent>();

    private event Action<AbilityState> EventStarter;


    #region PROPERITES
    protected abstract AbilityType AbilityType
    {
        get;
    }

    public AbilityParams AbilityParams
    {
        get => abilityParams;
    }

    public AbilityState AbilityState
    {
        get => abilityState;
        set
        {
            abilityState = value;
            EventStarter?.Invoke(value);
        }
    }

    #endregion


    protected virtual void Awake()
    {
        EventStarter += AbilityEventInvoke;
    }

    private void OnDestroy()
    {
        EventStarter -= AbilityEventInvoke;
    }

    public abstract void UseAbility();

    protected virtual IEnumerator StartUse(float time)
    {
        abilityState = AbilityState.Enabled;
        yield return new WaitForSeconds(time);
    }

    protected virtual IEnumerator Using(float time)
    {
        abilityState = AbilityState.Using;
        yield return new WaitForSeconds(time);
    }

    protected virtual IEnumerator CoolDown(float time)
    {
        abilityState = AbilityState.Disabled;
        yield return new WaitForSeconds(time);
    }


    private void AbilityEventInvoke(AbilityState currentState)
    {
        for (int i = 0; i < abilityEvent.Count; i++)
        {
           if(abilityEvent[i].CurrentAbilityState == currentState)
           {
               abilityEvent[i].Invoke();
           }
        }
    }


}



[System.Serializable]
public class AbilityParams
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
public class AbilityEvent
{
    [SerializeField] private AbilityState currentAbilityState;
    [SerializeField] private UnityEvent abilityEventOnState;

    #region PROPERTIES

    public AbilityState CurrentAbilityState { get => currentAbilityState; }

    #endregion

    public void Invoke()
    {
       abilityEventOnState?.Invoke();
    }

}