using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiliWeapon : AWeapon
{
    [SerializeField] private List<Collider> miliWeapons;
    [SerializeField] private float timeWeaponColider=1f;
    [SerializeField] private float timeToWeaponAttack = 0f;

    private bool weaponActive = false;

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
        if (!weaponActive)
        {
            
            isAttack = true;
            StartCoroutine(ChangeColider());    
        }
    }

    private IEnumerator ChangeColider()
    {
        weaponActive = true;
        yield return new WaitForSeconds(timeToWeaponAttack);

        events.OnAnimEvent(AnimationController.AnimationType.MeleAttack);       

        foreach (var weapon in miliWeapons)
        {
            weapon.enabled = weaponActive;
        }
        yield return new WaitForSeconds(timeWeaponColider);
        foreach (var weapon in miliWeapons)
        {
            weapon.enabled = weaponActive;
        }
        weaponActive = false;
        isAttack = false;


    }


    private void OnTriggerEnter(Collider other)
    {
    
        if(weaponActive)
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
