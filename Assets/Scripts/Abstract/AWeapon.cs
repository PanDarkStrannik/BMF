using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour, IWeapon
{

    [SerializeField] protected List<DamageByType> weaponData;
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected GameObject weaponObject;
    [SerializeField] protected MainEvents events;
    [SerializeField] protected LayerMask layer;


    protected bool isAttack = false;

    public WeaponType WeaponType
    {
        get
        {          
            return weaponType;
        }
    }

    public GameObject WeaponObject
    {
        get
        {
            return weaponObject;
        }
    }

    public bool IsAttack
    {
        get
        {
            return isAttack;
        }
    }


    public abstract void Attack();
     
}

public enum WeaponType
{
    Mili, Range
}