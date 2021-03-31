using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image reloadTimerImage;
    [SerializeField] private Text description;
    [SerializeField] private Text ammoCount;
    
    private PlayerController player;
    private List<UnityAction> actions = new List<UnityAction>();


    private void Start()
    {
        player = PlayerInformation.GetInstance().PlayerController;

        description.text = null;
        ActionCompare();
        player.OnPlayerInteractedSet += Player_OnPlayerInteractedSet;
        player.OnChangeWeapon += Player_OnChangeWeapon;
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged += ChangeAmmo;
       
    }

    private void Player_OnChangeWeapon(PlayerWeaponChanger.WeaponSpellsHolder obj)
    {
        if (obj.TryGetWeaponByType(out WeaponRange weaponRange))
        {
            ammoCount.enabled = true;
        }
        else
        {
            ammoCount.enabled = false;
        }
    }

    private void ChangeAmmo(DamagebleParam.ParamType paramType, float value, float maxValue)
    {
        if (paramType == DamagebleParam.ParamType.HolyWater)
        {
            ammoCount.text = value.ToString();
        }
    }

    private void Player_OnPlayerInteractedSet()
    {
        if(player.Interactable != null)
        {
           player.Interactable.OnDetect.AddListener(actions[0]);
           player.Interactable.OnUndetect.AddListener(actions[1]);
         
           if(player.Interactable is ReloadZone reloadZone)
           {
               reloadZone.OnReloading.AddListener(actions[2]);
           }
        }
    }

    private void ActionCompare()
    {
        actions.Add(new UnityAction(InteractableDescriptionIn));
        actions.Add(new UnityAction(InteractableDescriptionOut));
        actions.Add(new UnityAction(InteractableGettingDescription));
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
        player.Interactable.Unsubsribe(actions);
        //player.Interactable.Unsubscribe();
    }

    private void InteractableGettingDescription()
    {
        var reloadZone = player.Interactable as ReloadZone;
        reloadTimerImage.fillAmount = reloadZone.CurrentReloadTime / reloadZone.ReloadTime;
    }

    
}
