using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamagebleParamDatas
{
    [SerializeField] private List<DamagebleParam> paramDatas;

    public DamagebleParamDatas()
    {
        paramDatas = new List<DamagebleParam>();
    }


    public List<DamagebleParam> ParamDatas
    {
        get
        {
            return paramDatas;
        }
    }

    public int DatasCount
    {
        get
        {
            return paramDatas.Count;
        }
    }

    public DamagebleParam FindFirstByWeak(DamageType weak)
    {
        
        foreach (var data in paramDatas)
        {
            if(data.IsMyWeaknese(weak))
            {
                return data;
            }
        }
        return null;
    }

    public List<DamagebleParam> FindAllByWeak(DamageType weak)
    {
        
        List<DamagebleParam> tmp = new List<DamagebleParam>();
        foreach (var data in paramDatas)
        {
            if (data.IsMyWeaknese(weak))
            {
                tmp.Add(data);
            }
        }
        if (tmp.Count == 0)
        {
            return null;
        }
        else
        {
            return tmp;
        }
    }


    public DamagebleParam FindFirstByStrong(DamageType strong)
    {
        
        foreach (var data in paramDatas)
        {
            if (data.IsMyStrong(strong))
            {
                return data;
            }
        }
        return null;
    }


    public List<DamagebleParam> FindAllByStrong(DamageType strong)
    {
        
        List<DamagebleParam> tmp = new List<DamagebleParam>();
        foreach (var data in paramDatas)
        {
            if (data.IsMyStrong(strong))
            {
                tmp.Add(data);
            }
        }
        if (tmp.Count == 0)
        {
            return null;
        }
        else
        {
            return tmp;
        }
    }



    public DamagebleParam FindByParamType(DamagebleParam.ParamType type)
    {
        
        foreach (var data in paramDatas)
        {
            if(data.Type==type)
            {
                return data;
            }
        }
        return null;
    }

    public List<DamagebleParam> FindAllByParamType(DamagebleParam.ParamType type)
    {
       

        List<DamagebleParam> tmp = new List<DamagebleParam>();
        foreach (var data in paramDatas)
        {
            if (data.Type == type)
            {
                tmp.Add(data);
            }
        }
        if (tmp.Count == 0)
        {
            return null;
        }
        else
        {
            return tmp;
        }
    }

}


