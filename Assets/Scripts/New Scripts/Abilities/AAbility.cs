using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] protected GameObject abilityObject;

    protected abstract AbilityType AbilityType
    {
        get;
    }

    public AbilityState AbilityState = AbilityState.Enabled;


    public abstract void UseAbility();

    protected virtual IEnumerator StartUse(float time)
    {
        AbilityState = AbilityState.Enabled;
        yield return new WaitForSeconds(time);
    }

    protected virtual IEnumerator Using(float time)
    {
        AbilityState = AbilityState.Using;
        yield return new WaitForSeconds(time);
    }

    protected virtual IEnumerator CoolDown(float time)
    {
        AbilityState = AbilityState.Disabled;
        yield return new WaitForSeconds(time);
    }


   


}
