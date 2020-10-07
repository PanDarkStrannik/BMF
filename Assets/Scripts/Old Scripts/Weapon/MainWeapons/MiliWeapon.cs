﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiliWeapon : AWeapon
{
   // [SerializeField] private List<Collider> miliWeapons;
    [SerializeField] private float timeWeaponColider=1f;
    [SerializeField] private float timeToWeaponAttack = 0f;
    [SerializeReference] private DamageArea damageArea;


    private void Start()
    {
        //foreach(var weapon in miliWeapons)
        //{
        //    weapon.enabled = false;
        //    weapon.isTrigger = true;
        //}

        damageArea.AddDamage(weaponData);

    }


    public override void Attack()
    {
        if (state==AWeapon.WeaponState.Serenity)
        {
            state = AWeapon.WeaponState.Attack;
            StartCoroutine(ChangeColider());    
        }
    }

    private IEnumerator ChangeColider()
    {
        events.OnEffectEvent(EffectsController.EffectType.Melle, true);
        yield return new WaitForSecondsRealtime(timeToWeaponAttack);
        if (state==AWeapon.WeaponState.Attack)
        {
            events.OnAnimEvent(AnimationController.AnimationType.MeleAttack);

            damageArea.gameObject.SetActive(true);

            //foreach (var weapon in miliWeapons)
            //{
            //    weapon.enabled = true;
            //}
            yield return new WaitForSecondsRealtime(timeWeaponColider);
            damageArea.gameObject.SetActive(false);
            //foreach (var weapon in miliWeapons)
            //{
            //    weapon.enabled = false;
            //}
            state = AWeapon.WeaponState.Serenity;
        }
        events.OnEffectEvent(EffectsController.EffectType.Melle, false);

    }


    private void OnDisable()
    {
        state = AWeapon.WeaponState.Serenity;
       // StopCoroutine(ChangeColider());
    }

    //private void OnTriggerEnter(Collider other)
    //{

    //    if(state==AWeapon.WeaponState.Attack)
    //    {
    //        if(other.transform.GetComponent<IDamageble>()!=null)
    //        {
    //            if ((other.transform.GetComponent<ADamageble>().Layer.value & (1 << layer)) == 0)
    //            {
    //                Debug.Log("Что-то задели!");
    //                foreach (var data in weaponData)
    //                {
    //                    other.transform.GetComponent<IDamageble>().ApplyDamage(data);
    //                }
    //            }
    //        }
    //    }
    //}



}
