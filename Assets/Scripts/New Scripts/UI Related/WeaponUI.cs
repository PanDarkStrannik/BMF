using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image reloadTimerImage;
    [SerializeField] private Text description;
    [SerializeField] private Image waterFill;
    [SerializeField] private Image melFill;

    [SerializeField] private List<WeaponUISelection> weaponSelectionUI = new List<WeaponUISelection>();
    

    private PlayerController player;
    private List<UnityAction> actions = new List<UnityAction>();

    private float maxAbilityTime = 20f;


    private void Start()
    {
        ActionCompare();
        if(description != null)
        {
          description.text = null;
        }
    }

    private void OnEnable()
    {
        player = PlayerInformation.GetInstance().PlayerController;
        player.OnPlayerInteractedSet += Player_OnPlayerInteractedSet;
        player.OnChangeWeapon += Player_OnChangeWeapon;
        player.OnCurrentWeaponNumber += Player_OnCurrentWeaponNumber;
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged += ChangeAmmo;
    }

    

    private void OnDestroy()
    {
        player.OnPlayerInteractedSet -= Player_OnPlayerInteractedSet;
        player.OnChangeWeapon -= Player_OnChangeWeapon;
        player.OnCurrentWeaponNumber -= Player_OnCurrentWeaponNumber;
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged -= ChangeAmmo;
    }

    private void Player_OnCurrentWeaponNumber(int weaponNum)
    {
        foreach (var w in weaponSelectionUI)
        {
            if(w.CurrentWeapon == weaponNum)
            {
                w.Invoke();
            }
        }
    }

    private void Player_OnChangeWeapon(PlayerWeaponChanger.WeaponSpellsHolder curWeapon)
    {
        // перекати-поле...
    }

    private void ChangeAmmo(DamagebleParam.ParamType paramType, float value, float maxValue)
    {
        if (paramType == DamagebleParam.ParamType.HolyWater)
        {
            waterFill.fillAmount = value / maxValue;
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

[System.Serializable]
public class WeaponUISelection
{
    public string name;
    public int CurrentWeapon;
    public UnityEvent OnCurrentWeaponSelected;

    public void Invoke()
    {
        OnCurrentWeaponSelected?.Invoke();
    }
}