using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHealing : AWeapon
{
    [SerializeField] protected Transform gunPosition; //пока не используется
    [SerializeField] private List<DamageByType> weaponData;
    [SerializeField] protected LayerMask layer;
    [SerializeField] protected float attackTime;
    [SerializeField] protected float reloadTime = 0f;
    [SerializeField] private float healCount = 0;

    private int currentHealCount = 0;

    public override WeaponType WeaponType
    {
        get
        {
            return WeaponType.Directional;
        }
    }


    public override void Attack(GameObject healingObject)
    {
        if (state != WeaponState.ImposibleAttack && currentHealCount >= healCount)
        {
            StopCoroutine(Damaging(attackTime,healingObject));
            StartCoroutine(Reload(reloadTime));
        }
        else if (state == WeaponState.Serenity)
        {
            currentHealCount++;
            StartCoroutine(Damaging(attackTime, healingObject));
        }
    }


    protected override IEnumerator Damaging(float time, GameObject healingObject)
    {
        State = WeaponState.Attack;
        //if (healingobject.getcomponent<paramcontroller>() != null)
        //{
        //    foreach (var healtype in weapondata)
        //    {
        //        healingobject.getcomponent<paramcontroller>().damagebleparams.healallbytype(healtype);
        //    }
        //}

        if(FindComponentInIerarhy<ParamController>(healingObject, out ParamController finded))
        {
            foreach(var healType in weaponData)
            {
                finded.DamagebleParams.HealAllByType(healType);
            }
        }

        yield return new WaitForSecondsRealtime(time);
        StartCoroutine(Serenity(0f));
    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        if (state == WeaponState.Reload)
        {
            yield return new WaitForSecondsRealtime(time); 
            currentHealCount = 0;
            StartCoroutine(Serenity(0f));
        }
    }

    protected override IEnumerator Serenity(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        State = WeaponState.Serenity;
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        state = WeaponState.Serenity;
    }

    private bool FindComponentInIerarhy<T>(GameObject searchObject, out T finded) where T : MonoBehaviour
    {
        finded = default(T);
        if (searchObject.GetComponent<T>() != null)
        {
            finded = searchObject.GetComponent<T>();            
            return true;
        }
        else if (searchObject.GetComponentInParent<T>() != null)
        {
            finded = searchObject.GetComponentInParent<T>();
            return true;
        }
        else if (searchObject.GetComponentInChildren<T>() != null)
        {
            finded = searchObject.GetComponentInChildren<T>();
            return true;
        }
        return false;
    }
}
