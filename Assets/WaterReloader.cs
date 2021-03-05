using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReloader : MonoBehaviour
{
    public static bool isReadyToReload = false;
    private void OnTriggerStay(Collider other)
    {
        WeaponWater water = other.GetComponent<WeaponWater>();

        if(water != null)
        {
            isReadyToReload = true;
            Debug.Log("water is here");
            water.Reload();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        WeaponWater water = other.GetComponent<WeaponWater>();

        if (water != null)
        {
            isReadyToReload = false;
        }
    }
}
