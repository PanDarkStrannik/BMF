using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiliWeapon : AWeapon
{
    [SerializeField] private List<Collider> miliWeapons;
    [SerializeField] private float timeWeaponColider=1f;
    [SerializeField] private float timeToWeaponAttack = 0f;


    private void Start()
    {
        foreach(var weapon in miliWeapons)
        {
            weapon.enabled = false;
            weapon.isTrigger = true;
        }

    }


    public override void Attack()
    {
        if (!isAttack)
        { 
            isAttack = true;
            StartCoroutine(ChangeColider());    
        }
    }

    private IEnumerator ChangeColider()
    {
        yield return new WaitForSecondsRealtime(timeToWeaponAttack);
        if (isAttack)
        {
            events.OnAnimEvent(AnimationController.AnimationType.MeleAttack);

            foreach (var weapon in miliWeapons)
            {
                weapon.enabled = isAttack;
            }
            yield return new WaitForSecondsRealtime(timeWeaponColider);

            isAttack = false;
            foreach (var weapon in miliWeapons)
            {
                weapon.enabled = isAttack;
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
    
        if(isAttack)
        {
            if(other.transform.GetComponent<IDamageble>()!=null)
            {
                if ((other.transform.GetComponent<ADamageble>().Layer.value & (1 << layer)) == 0)
                {
                    Debug.Log("Что-то задели!");
                    foreach (var data in weaponData)
                    {
                        other.transform.GetComponent<IDamageble>().ApplyDamage(data);
                    }
                }
            }
        }
    }


   
}
