using UnityEngine;

[System.Serializable]
public class DamageByType
{
    [SerializeField] private DamageType damageType;

    [Range(0, 10000)]
    [SerializeField] private float value;

    public DamageType DamageType
    {
        get
        {
            return damageType;
        }
    }

    public float Value
    {
        get
        {
            return value;
        }
    }


    public DamageByType()
    {
    }    

    public DamageByType(DamageType damageType, float value)
    {
        this.damageType = damageType;
        this.value = value;

    }
}

public enum DamageType
{
    Magic, Physical, Mental,  Other
}