using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveAbility : AAbility
{
    [SerializeField] protected ParamController paramController;


    protected override AbilityType AbilityType => AbilityType.ActiveSkill;


    public override void UseAbility()
    {
        if(abilityState == AbilityState.Enabled)
        {
            Debug.Log("Ability use start");
            StopAllCoroutines();
            StartCoroutine(StartUse(abilityParams.TimeForUse));
        }
    }


    protected override IEnumerator StartUse(float time)
    {
        abilityState = AbilityState.Enabled;
        if(abilityState == AbilityState.Enabled)
        {
            yield return new WaitForSecondsRealtime(time); // time for Start
            StartCoroutine(Using(abilityParams.ActiveTime));
        }

    }


    protected override IEnumerator Using(float time)
    {
        abilityState = AbilityState.Using;
        if(abilityState == AbilityState.Using)
        {
            yield return new WaitForSecondsRealtime(time); //active time
            StartCoroutine(CoolDown(abilityParams.CoolDownTime));
        }
    }

    protected override IEnumerator CoolDown(float time)
    {
       if(abilityState != AbilityState.Disabled)
       {
            abilityState = AbilityState.Disabled;
            yield return new WaitForSecondsRealtime(time); // coolDown time
            Debug.Log("Ability is ready!");
            StopAllCoroutines();
            abilityState = AbilityState.Enabled;
       }
    }

}


