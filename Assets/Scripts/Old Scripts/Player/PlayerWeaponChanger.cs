using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerWeaponChanger
{
   // [SerializeReference] private List<AWeapon> weapons;
    [SerializeField] private List<WeaponSpellsHolder> weapons;

   // private AWeapon currentWeapon = null;

    private WeaponSpellsHolder currentWeapon = null;

    private int currentWeaponNum = 0;

    public WeaponSpellsHolder CurrentWeapon
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

    public List<WeaponSpellsHolder> AllWeapons
    {
        get
        {
            return weapons;
        }
    }

    public bool TryGetCurrentWeaponHolder(out WeaponSpellsHolder returnedHolder)
    {
        returnedHolder = currentWeapon;
        if(currentWeapon==null)
        {
            return false;
        }
        return true; 
    }

    public void NextWeapon()
    {
        ChangeWeapon(currentWeaponNum + 1);
    }

    public void PrevWeapon()
    {
        ChangeWeapon(currentWeaponNum - 1);
    }

    public void ChangeWeapon(int weaponNum)
    {
        Debug.Log("Номер оружия: " + weaponNum);
        if (weapons.Count > 0)
        {
            if (weaponNum > weapons.Count - 1)
            {
                weaponNum = 0;
            }
            else if (weaponNum<0)
            {
                weaponNum = weapons.Count - 1;
            }


            if (currentWeapon != null)
            {
                if (currentWeapon.TrueStateForOne(AWeapon.WeaponState.Attack))
                {
                    return;
                }

                if (currentWeapon != weapons[weaponNum])
                {
                    currentWeapon.DisableWeaponObject();
                }
            }
            currentWeapon = weapons[weaponNum];
            currentWeapon.EnableWeaponObject();
            currentWeaponNum = weaponNum;

        }
        else
        {
            throw new System.Exception("Нет оружия для смены!");
        }
        
    }

    [System.Serializable]
    public class WeaponSpellsHolder
    {
        [SerializeReference] private AWeapon weapon1;
        [SerializeReference] private AWeapon weapon2;
        [SerializeReference] private GameObject weaponObject;

        public AWeapon Weapon1
        {
            get
            {
                return weapon1;
            }
        }

        public AWeapon Weapon2
        {
            get
            {
                return weapon2;
            }
        }


        public void DisableWeaponObject()
        {
            weaponObject.SetActive(false);
        }

        public void EnableWeaponObject()
        {
            weaponObject.SetActive(true);
        }

        public bool TryUseFirstWeapon()
        {
            if (weapon1 != null)
            {
                if (weapon1.State == AWeapon.WeaponState.Serenity)
                {
                    weapon1.UseWeapon();
                    return true;
                }
            }
            return false;
        }

        public bool TryUseSecoundWeapon()
        {
            if (weapon2 != null)
            {
                if (weapon2.State == AWeapon.WeaponState.Serenity)
                {
                    weapon2.UseWeapon();
                    return true;
                }
            }
            return false;
        }

        public bool TryGetWeaponByType<T>(out T returnedWeapon) where T : class
        {
            if(weapon1.TryReturnNeededWeaponType(out T component1))
            {
                returnedWeapon = weapon1 as T;
                return true;
            }
            else if(weapon2.TryReturnNeededWeaponType(out T component2))
            {
                returnedWeapon = weapon2 as T;
                return true;
            }
            returnedWeapon = null;
            return false;
        }
        public bool TrueStateForOne(AWeapon.WeaponState state)
        {
            if(weapon1 != null)
            {
                if (weapon1.State == state)
                {
                    return true;
                }
            }
            else if (weapon2 != null)
            {
                if (weapon2.State == state)
                {
                    return true;
                }
            }
            return false;
        }
        public bool TrueStateForBoth(AWeapon.WeaponState state)
        {
            if(weapon1.State==state && weapon2.State == state)
            {
                return true;
            }
            return false;
        }
    }

}
