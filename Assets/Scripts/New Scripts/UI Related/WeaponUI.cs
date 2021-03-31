using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image reloadTimerImage;
    [SerializeField] private Text description;
    
    private PlayerController player;
    private UnityAction action;


    private void Start()
    {
        player = PlayerInformation.GetInstance().PlayerController;

        description.text = null;
        player.OnPlayerInteractedSet += Player_OnPlayerInteractedSet;
        ActionCompare();
       
    }

    private void Player_OnPlayerInteractedSet()
    {
        if(player.Interactable != null)
        {
           player.Interactable.OnDetect.AddListener(InteractableDescriptionIn);
           player.Interactable.OnUndetect.AddListener(InteractableDescriptionOut);
         
           if(player.Interactable is ReloadZone reloadZone)
           {
               reloadZone.OnReloading.AddListener(InteractableGettingDescription);
           }
        }
    }

    private void ActionCompare()
    {
        action += Player_OnPlayerInteractedSet;
        action += InteractableDescriptionIn;
        action += InteractableDescriptionOut;
        action += InteractableGettingDescription;
    }

    private void InteractableDescriptionIn()
    {
        if(player.Interactable == null)
        {
            Debug.Log("interactable null");
        }
        description.text = player.Interactable.GetDescription();
    }

    private void InteractableDescriptionOut()
    {
        description.text = null;
        // player.Interactable.Unsubsribe(action);
        player.Interactable.Unsubscribe();
    }

    private void InteractableGettingDescription()
    {
        var reloadZone = player.Interactable as ReloadZone;
        reloadTimerImage.fillAmount = reloadZone.CurrentReloadTime / reloadZone.ReloadTime;
    }
}
