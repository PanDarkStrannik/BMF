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

    protected WeaponState state = WeaponState.Serenity;


    protected bool isAttack = false;

    public WeaponType WeaponType
    {
        get
        {          
            return weaponType;
        }
    }

    public WeaponState State
    {
        get
        {
            return state;
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

    public virtual IEnumerator StopAttack(float stopTime)
    {
        Debug.Log("Остановили Атаку!");
        StopAllCoroutines();
        state = WeaponState.ImposibleAttack;
        yield return new WaitForSeconds(stopTime);
        state = WeaponState.Serenity;
        Debug.Log("Разрешили Атаку!");
    }

    public enum WeaponState
    {
        Attack, Reload, ImposibleAttack, Serenity
    }
     
}

public enum WeaponType
{
    Mili, Range
}