﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponLogic : MonoBehaviour
{
    [SerializeField] private List<WeaponDistance> weapons;


    private WeaponDistance currentWeapon;


    public WeaponDistance CurrentWeapon
    {
        get
        {
            return currentWeapon;
        }
    }

    private GameObject wantDamagedObject;
    public GameObject WantDamagedObject
    {
        get
        {
            return wantDamagedObject;
        }
    }


    private void Start()
    {
        currentWeapon = weapons[0];
    }


    public void AddWeapon(WeaponDistance weapon)
    {
        weapons.Add(weapon);
        weapon.Weapon.WeaponObject.SetActive(false);
    }

    public bool SelectWeapon(WeaponType weaponType)
    {
        if (currentWeapon.Weapon.State == AWeapon.WeaponState.Serenity)
        {
            var temp = currentWeapon;
            List<WeaponDistance> weaponDistancesTemp = new List<WeaponDistance>();

            foreach (var weapon in weapons)
            {
                if (weapon.Weapon.WeaponType == weaponType)
                {
                    weaponDistancesTemp.Add(weapon);
                    //temp = weapon;
                    //break;
                }
            }
            if (weaponDistancesTemp.Count > 1)
            {
                var rand = Random.Range(0, weaponDistancesTemp.Count);
                temp = weaponDistancesTemp[rand];
            }
            else if (weaponDistancesTemp.Count == 1)
            {
                temp = weaponDistancesTemp[0];
            }
            else
            {
                Debug.LogError("Нет подходящего оружия");
                return false;
            }

            if (temp != null && currentWeapon.Weapon.State == AWeapon.WeaponState.Serenity)
            {
                currentWeapon.Weapon.WeaponObject.SetActive(false);
                currentWeapon = temp;
                currentWeapon.Weapon.WeaponObject.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public bool Attack()
    {
        if(Physics.SphereCast(currentWeapon.Point.position, currentWeapon.Radius,
            currentWeapon.Point.forward, out RaycastHit hit, currentWeapon.Distance) &&
            currentWeapon.Weapon.State == AWeapon.WeaponState.Serenity)
        {
            if (hit.transform.GetComponent<IDamageble>() != null && currentWeapon.Weapon.State == AWeapon.WeaponState.Serenity)
            {
                wantDamagedObject = hit.transform.gameObject;
                Debug.Log("По идее произошла атака");
                currentWeapon.Weapon.Attack();
                return true;
            }
        }
        return false;
    }



    public bool Attack(WeaponType weaponType)
    {
        if(SelectWeapon(weaponType))
        {
            if(Attack())
            {
                return true;
            }
        }
        return false;
    }


    private void OnDrawGizmos()
    {
        foreach (var weapon in weapons)
        {
            if (weapon.GizmosColor != null && weapon.Point != null)
            {
                var tmpColor = weapon.GizmosColor;
                var tmpPoint = weapon.Point;
                var tmpRadius = weapon.Radius;
                var tmpDistance = weapon.Distance;
                Gizmos.color = tmpColor;       
                Gizmos.DrawSphere(tmpPoint.position, tmpRadius);
                Gizmos.DrawSphere(tmpPoint.position + tmpPoint.forward * tmpDistance, tmpRadius);
            }
        }
    }
}

[System.Serializable]
public class WeaponDistance
{
    [SerializeReference] private AWeapon weapon;
    [SerializeField] private Transform point;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float distance = 1f;
    [SerializeField] private Color gizmosColor;

    public AWeapon Weapon
    {
        get
        {
            return weapon;
        }
    }
    public Transform Point
    {
        get
        {
            return point;
        }
    }
    public float Radius
    {
        get
        {
            return radius;
        }
    }
    public float Distance
    {
        get
        {
            return distance;
        }
    }
    public Color GizmosColor
    {
        get
        {
            return gizmosColor;
        }
    }

    public WeaponDistance()
    {
        radius = 1f;
        distance = 1;
        gizmosColor = Color.black;
    }

}