using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamagebleParamSum
{
    [SerializeField] private List<DamagebleParam.ParamType> types;
    [SerializeReference] private List<ADamageble> damagebles;

    private DamagebleParamDatas sumDatas;

    public Dictionary<DamagebleParam.ParamType, float> typesValues;
    public Dictionary<DamagebleParam.ParamType, float> typesMaxValues;


    public delegate void ParamNullHelper(DamagebleParam.ParamType paramType);
    public event ParamNullHelper OnParamNull;

    public delegate void ParamChangedHelper(DamagebleParam.ParamType paramType, float value, float maxValue);
    public event ParamChangedHelper OnParamChanged;

    public void Initialize()
    {
        sumDatas = new DamagebleParamDatas();

        typesValues = new Dictionary<DamagebleParam.ParamType, float>();
        typesMaxValues = new Dictionary<DamagebleParam.ParamType, float>();
        if (damagebles.Count > 0)
        {


            foreach (var placeDamage in damagebles)
            {
                sumDatas.ParamDatas.AddRange(placeDamage.Datas.ParamDatas);
                placeDamage.OnDamaged += ChangeParam;

            }

            ChangeParam();
        }

    }

    public void Unsubscribe()
    {
        if (damagebles.Count > 0)
        {
            foreach (var placeDamage in damagebles)
            {                
                placeDamage.OnDamaged += ChangeParam;
            }
        }
    }

    private void ChangeParam()
    {
        typesValues.Clear();
        typesMaxValues.Clear();
        foreach (var type in types)
        {
            var temp = sumDatas.FindAllByParamType(type);
            var value = 0f;
            var maxValue = 0f;
            if (temp != null)
            {
                foreach (var e in temp)
                {
                    value += e.Value;
                    maxValue += e.MaxValue;
                }
            }

            if (value <= 0)
            {
                value = 0;
                OnParamNull?.Invoke(type);
            }

            typesValues.Add(type, value);
            typesMaxValues.Add(type, maxValue);

            OnParamChanged?.Invoke(type, value, maxValue);
        }
    }

}
