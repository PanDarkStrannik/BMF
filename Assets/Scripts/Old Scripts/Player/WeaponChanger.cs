using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponChanger
{
    [SerializeReference] private List<AWeapon> weapons;

    private AWeapon currentWeapon = null;

    private float currentWeaponNum = 0f;

    public AWeapon CurrentWeapon
    {
        get
        {
            if (currentWeapon != null)
            {
                return currentWeapon;
            }
            return null;
        }
    }

    public List<AWeapon> AllWeapons
    {
        get
        {
            return weapons;
        }
    }


    public void NextWeapon()
    {
        currentWeaponNum++;

        if (currentWeaponNum > weapons.Count - 1)
        {
            currentWeaponNum = 0;
        }

        ChangeWeapon(currentWeaponNum);
    }

    public void PrevWeapon()
    {
        currentWeaponNum--;

        if (currentWeaponNum < 0)
        {
            currentWeaponNum = weapons.Count - 1;
        }        
        ChangeWeapon(currentWeaponNum);
    }


    public void ChangeWeapon(float weaponNum)
    {
        Debug.Log("Номер оружия: " + weaponNum);
        if (weapons.Count > 0)
        {
            if (weaponNum > weapons.Count - 1)
            {
                weaponNum = 0;
            }

            if (currentWeapon != null)
            {
                if(currentWeapon.State == AWeapon.WeaponState.Attack)
                {
                    return;
                }
            }
            if (currentWeapon != null && currentWeapon != weapons[(int)weaponNum])
            {
                currentWeapon.WeaponObject.SetActive(false);
            }
            currentWeapon = weapons[(int)weaponNum];
            currentWeapon.WeaponObject.SetActive(true);
            currentWeaponNum = weaponNum;

        }
        else
        {
            throw new System.Exception("Нет оружия для смены!");
        }
        
    }


}
