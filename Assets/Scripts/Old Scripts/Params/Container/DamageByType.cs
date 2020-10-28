using UnityEngine;

[System.Serializable]
public class DamageByType
{
    [SerializeField] private DamageType damageType;

    [Range(0, 10000)]
    [SerializeField] private float damageValue;

    //[SerializeField] private ForceMode forceMode;

    //[SerializeField] private float forceValue;

    public DamageType DamageType
    {
        get
        {
            return damageType;
        }
    }

    public float DamageValue
    {
        get
        {
            return damageValue;
        }
    }

    //public ForceMode ForceMode
    //{
    //    get
    //    {
    //        return forceMode;
    //    }
    //}

    //public float ForceValue
    //{
    //    get
    //    {
    //        return forceValue;
    //    }
    //}


    public DamageByType()
    {
    }    

    public DamageByType(DamageType damageType, float value)
    {
        this.damageType = damageType;
        this.damageValue = value;

    }


    public void AddDamage(float damage)
    {
        damageValue += damage;
    }

}

public enum DamageType
{
    Magic, Physical, Mental,  Other
}