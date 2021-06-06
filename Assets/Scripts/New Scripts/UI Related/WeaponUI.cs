using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;


public class WeaponUI : MonoBehaviour
{
    [Header("Axe")]
    [SerializeField] private Image axeHeavyAttackCoolDownImage;
    private bool isReloading = false;


    [Header("Holy Water")]
    [SerializeField] private Image waterReloadTimerImage;
    [SerializeField] private TMP_Text waterReloadDescriptionText;
    [SerializeField] private Image waterFill;


    [Header("Icon change")]
    [SerializeField] private List<WeaponUISelection> weaponSelectionUI = new List<WeaponUISelection>();

    private PlayerController player;
    private List<UnityAction> actions = new List<UnityAction>();



    private void Start()
    {
        ActionCompare();
        if(waterReloadDescriptionText != null)
        {
          waterReloadDescriptionText.text = null;
        }
    }

    private void OnEnable()
    {
        player = PlayerInformation.GetInstance().PlayerController;
        player.OnPlayerInteractedSet += Player_OnPlayerInteractedSet;
        player.OnCurrentWeaponNumber += Player_OnCurrentWeaponNumber;
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged += ChangeAmmo;
    }

    

    private void OnDestroy()
    {
        player.OnPlayerInteractedSet -= Player_OnPlayerInteractedSet;
        player.OnCurrentWeaponNumber -= Player_OnCurrentWeaponNumber;
        PlayerInformation.GetInstance().PlayerParamController.DamagebleParams.OnParamChanged -= ChangeAmmo;
    }



    public void ShowAxeReloadTime()
    {
        StartCoroutine(AxeReloadTick(1));
    }


    private IEnumerator AxeReloadTick(float maxTime)
    {
        isReloading = true;

        for (float i = 0; i < maxTime; i+= Time.deltaTime)
        {
            float startTime = i;
            axeHeavyAttackCoolDownImage.fillAmount = startTime / maxTime;
            if(!isReloading)
            {
                startTime = 0;
            }
            yield return new WaitForEndOfFrame();
        }
        isReloading = false;
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
        waterReloadDescriptionText.text = player.Interactable.GetDescription();
    }

    private void InteractableDescriptionOut()
    {
        waterReloadDescriptionText.text = null;
        player.Interactable.Unsubsribe(actions);
        //player.Interactable.Unsubscribe();
    }

    private void InteractableGettingDescription()
    {
        var reloadZone = player.Interactable as ReloadZone;
        waterReloadTimerImage.fillAmount = reloadZone.CurrentReloadTime / reloadZone.ReloadTime;
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