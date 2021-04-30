using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mel : ActiveAbility
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private SpawnedObject spawnShield;

    private void Awake()
    {
        spawner.CreateSpawner();
        foreach (var s in spawner.spawned_objects)
        {
            spawnShield = s.GetComponent<SpawnedObject>();
        }
    }

    public override void UseAbility()
    {
        if(AbilityState == AbilityState.Enabled)
        {
            StartCoroutine(StartUse(activeSkillParams.TimeForUse));
        }
    }

    protected override IEnumerator StartUse(float time)
    {
        AbilityState = AbilityState.Enabled;
        if(AbilityState == AbilityState.Enabled)
        {
            yield return new WaitForSecondsRealtime(time);
            var melPos = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
            spawner.SpawnFirstObjectInQueue(melPos, Quaternion.identity);
            activeSkillEvent.InvokeAbilityEvent(AbilityState);
            StartCoroutine(Using(activeSkillParams.ActiveTime));
        }
    }

    protected override IEnumerator Using(float time)
    {
        AbilityState = AbilityState.Using;
        if(AbilityState == AbilityState.Using)
        {
            yield return new WaitForSecondsRealtime(time);
            activeSkillEvent.InvokeAbilityEvent(AbilityState);
            spawnShield.Die();
            StartCoroutine(CoolDown(activeSkillParams.CoolDownTime));
        }
    }

    protected override IEnumerator CoolDown(float time)
    {
        if(AbilityState != AbilityState.Disabled)
        {
            AbilityState = AbilityState.Disabled;
            yield return new WaitForSecondsRealtime(time);
            StopAllCoroutines();
            AbilityState = AbilityState.Enabled;
        }
    }
}
