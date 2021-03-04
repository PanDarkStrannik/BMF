using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        
        if(player != null)
        {
            player.controlMoveType = PlayerController.ControlMoveType.Vertical;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            player.controlMoveType = PlayerController.ControlMoveType.Ground;
        }
    }

}
