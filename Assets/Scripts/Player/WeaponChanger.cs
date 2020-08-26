using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponChanger
{
    [SerializeReference] private List<AWeapon> weapons;

    private AWeapon currentWeapon = null;

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

    public void ChangeWeapon(float weaponNum)
    {
        if (weapons.Count > 0)
        {
            if (weaponNum > weapons.Count)
            {
                weaponNum = 0;
            }

            if (currentWeapon != null)
            {
                if(currentWeapon.IsAttack)
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

        }
        
    }


}
