using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


public class WeaponUI : MonoBehaviour
{
    [Header("Holy Water")]
    [SerializeField] private Image waterReloadTimerImage;
    [SerializeField] private TMP_Text waterReloadDescriptionText;
    [SerializeField] private Image waterFill;

    [Header("Mel")]
    [SerializeField] private Image melFill;
    private bool isMelReloading = false;

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

    #region  Либо убрать в другой класс,либо хз чо

    public void MelDecrement()
    {
        StopAllCoroutines();
        for (int i = 0; i < player.Abilities.Count; i++)
        {
            if (player.Abilities[i] is Mel)
            {
                StartCoroutine(MelDecrementTime(player.Abilities[i].AbilityParams.ActiveTime));
            }
        }
       // StartCoroutine(MelDecrementTime(player.Abilities.AbilityParams.ActiveTime));
    }
    
    public void MelIncrement()
    {
        StopAllCoroutines();
        for (int i = 0; i < player.Abilities.Count; i++)
        {
            if(player.Abilities[i] is Mel)
            {
                StartCoroutine(MelIncrementTime(player.Abilities[i].AbilityParams.CoolDownTime));
            }
        }
       // StartCoroutine(MelIncrementTime(player.Abilities.AbilityParams.CoolDownTime));
    }

    private IEnumerator MelIncrementTime(float maxTime)
    {
        isMelReloading = true;
        for (float i = 0; i < maxTime; i+= Time.deltaTime)
        {
            var startTime = i;
            melFill.fillAmount = startTime / maxTime;
            if(!isMelReloading)
            {
                startTime = 0f;
            }
            yield return new WaitForEndOfFrame();
        }
        isMelReloading = false;
    }

    private IEnumerator MelDecrementTime(float maxTime)
    {
        var startTime = 0f;
        isMelReloading = true;
        for (float i = maxTime; 0f <= startTime; i -= Time.deltaTime)
        {
            startTime = i;
            melFill.fillAmount = i / maxTime;
            if(!isMelReloading)
            {
                startTime = 0f;
            }
            yield return new WaitForEndOfFrame();
        }
        isMelReloading = false;
    }

    #endregion

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