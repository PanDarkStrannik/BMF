using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParamController : MonoBehaviour
{
    [SerializeField] protected DamagebleParamSum paramSum;

    [SerializeField] protected float timeToDeactive=1f;

    [SerializeField] protected List<MonoBehaviour> deactiveScripts;

    private void Awake()
    {
        paramSum.OnParamNull += CheckType;
        paramSum.OnParamChanged += CheckTypeAndValues; 
    }

    private void OnEnable()
    {
        paramSum.Initialize();
    }

    private void OnDisable()
    {
        paramSum.Unsubscribe();
        paramSum.OnParamNull -= CheckType;
        paramSum.OnParamChanged -= CheckTypeAndValues;
    }

    private void CheckType(DamagebleParam.ParamType type)
    {
        switch (type)
        {
            case DamagebleParam.ParamType.Health:
                StartCoroutine(NullHealth());
                break;
        }
    }

    protected virtual IEnumerator NullHealth()
    {
        foreach (var e in deactiveScripts)
        {
            e.enabled = false;
        }
        yield return new WaitForSeconds(timeToDeactive);
        foreach(var e in deactiveScripts)
        {
            e.gameObject.SetActive(false);
        }
    }

    protected virtual void CheckTypeAndValues(DamagebleParam.ParamType type, float value, float maxValue)
    {
        switch (type)
        {
            case DamagebleParam.ParamType.Health:
                Debug.Log($"{type} + {value} + {maxValue}");
                break;
        }
    }

}
