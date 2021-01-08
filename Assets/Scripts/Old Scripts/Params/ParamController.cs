using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ParamController : MonoBehaviour
{
    [SerializeField] protected DamagebleParamSum paramSum;

    [SerializeField] protected float timeToDeactive=1f;

    [SerializeField] protected List<MonoBehaviour> deactiveScripts;

    [SerializeReference] private Rigidbody body;

    [SerializeReference] private AFaling faling;

    [SerializeField] private List<HeigthAndDamage> heigthsAndDamages;

    

    //private void Awake()
    //{
    //    paramSum.OnParamNull += CheckType;
    //    paramSum.OnParamChanged += CheckTypeAndValues; 
    //}

    public DamagebleParamSum DamagebleParams
    {
        get
        {
            return paramSum;
        }
    }

    protected virtual void OnEnable()
    {
        heigthsAndDamages.OrderBy(x => x.Heigth);
        heigthsAndDamages.Reverse();
        paramSum.OnParamNull += CheckType;
        paramSum.OnParamChanged += CheckTypeAndValues;
        faling.FallEvent += FallingDamage;

        var tempADamagebles = paramSum.Damagebles;
        foreach (var damageble in tempADamagebles)
        {
            damageble.PushEvent += Pusher;
        }

        foreach (var e in deactiveScripts)
        {
            e.enabled = true;
        }

        paramSum.Initialize();
    }

    //private void Start()
    //{
    //    heigthsAndDamages.OrderBy(x => x.Heigth);
    //    heigthsAndDamages.Reverse();
    //    paramSum.OnParamNull += CheckType;
    //    paramSum.OnParamChanged += CheckTypeAndValues;
    //    faling.FallEvent += FallingDamage;

    //    var tempADamagebles = paramSum.Damagebles;
    //    foreach (var damageble in tempADamagebles)
    //    {
    //        damageble.PushEvent += Pusher;
    //    }

    //    paramSum.Initialize();
    //}

    protected virtual void OnDisable()
    {
        paramSum.Unsubscribe();
        paramSum.OnParamNull -= CheckType;
        paramSum.OnParamChanged -= CheckTypeAndValues;

        faling.FallEvent -= FallingDamage;

        var tempADamagebles = paramSum.Damagebles;
        foreach (var damageble in tempADamagebles)
        {
            damageble.PushEvent -= Pusher;
        }
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
        yield return new WaitForSeconds(timeToDeactive);
        foreach (var e in deactiveScripts)
        {
            e.enabled = false;
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

    protected void Pusher(Vector3 direction, ForceMode forceMode)
    {        
        body.AddForce(direction, forceMode);
    }

    protected void FallingDamage(float heigth)
    {
        Debug.Log("Сработало падение!");
        for (int i = 0; i < heigthsAndDamages.Count; i++)
        {
            if (Math.Abs(heigth) >= heigthsAndDamages[i].Heigth)
            {
                paramSum.DamageAllByType(heigthsAndDamages[i].Damage);
                Debug.Log("Урон от падения: " + heigthsAndDamages[i].Damage.DamageValue);
                break;
            }
        }
    }


    [System.Serializable]
    public class HeigthAndDamage
    {
        [SerializeField] private float heigth = 0f;
        [SerializeField] private DamageByType damage;

        public float Heigth
        {
            get
            {
                return heigth;
            }
        }

        public DamageByType Damage
        {
            get
            {
                return damage;
            }
        }

    }

    [System.Serializable]
    public class ResourcesUser
    {
        [SerializeReference] private ParamController paramController;
        [SerializeField] private DamagebleParam.ParamType paramType;
        [SerializeField] private float minValueToUse;
        [SerializeField] private float changeParamOnUse;

        public ParamController ParamController
        {
            get
            {
                return paramController;
            }
        }

        public bool TryUseResource()
        {
            if(paramController.paramSum.typesValues.ContainsKey(paramType))
            {
                if (paramController.paramSum.typesValues[paramType] >= minValueToUse)
                {
                    paramController.paramSum.ChangeParam(paramType, changeParamOnUse);
                    return true;
                }
                Debug.Log("В Param Controller ресурса " + paramType + " меньше чем " + minValueToUse);
                return false;
            }
            Debug.Log("Param controller не содержит " + paramType);
            return false;
        }
    }

}
