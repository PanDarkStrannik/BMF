using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private DamageblePlace shieldDamagePlace;
    [SerializeField] private DamageblePlace playerDamagePlace;


    [SerializeField] private LayerMask playerShieldIn;
    [SerializeField] private LayerMask playerShieldOff;

    [SerializeField] private LayerMask shieldIn;
    [SerializeField] private LayerMask shieldOff;



    private void OnTriggerEnter(Collider other)
    {
      var player = other.GetComponentInParent<PlayerController>();
        if(player != null)
        {
            if(shieldDamagePlace != null && playerDamagePlace != null)
            {
                //shieldDamagePlace.Layer = shieldIn;
                //playerDamagePlace.Layer = playerShieldIn;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
      var player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            if (shieldDamagePlace != null && playerDamagePlace != null)
            {
                //shieldDamagePlace.Layer = shieldOff;
                //playerDamagePlace.Layer = playerShieldOff;
            }
        }
    }
}
