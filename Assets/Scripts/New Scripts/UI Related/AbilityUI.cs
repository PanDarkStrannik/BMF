using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
public class AbilityUI : MonoBehaviour
{
    [SerializeField] private TMP_Text onAbilityTookText;
    [SerializeField] private TMP_Text onAbilityNullText;

    [Header("Chalk")]
    [SerializeField] private Image ChalkTimeFill;
    [SerializeField] private Image ChalkHealthFiil;
    private bool isMelReloading = false;

    private PlayerController player;
    private ShieldParamController shieldParamController;


    private void Awake()
    {
        player = PlayerInformation.GetInstance().PlayerController;
    }

    private void OnEnable()
    {
        player.OnAbilityTook += Player_OnAbilityTook;
        player.OnAbilityNull += Player_OnAbilityNull;
        ShieldParamController.OnShieldDestroyed += ShieldDestroy;
        ChalkAbility.OnShieldGet += GetShield;
    }


    private void OnDisable()
    {
        player.OnAbilityTook -= Player_OnAbilityTook;
        player.OnAbilityNull -= Player_OnAbilityNull;
        ShieldParamController.OnShieldDestroyed -= ShieldDestroy;
        ChalkAbility.OnShieldGet -= GetShield;
        shieldParamController.DamagebleParams.OnParamChanged -= DamagebleParams_OnParamChanged;
    }

    

    private void Start()
    {
        if(shieldParamController != null)
        {
            shieldParamController.DamagebleParams.OnParamChanged += DamagebleParams_OnParamChanged;
        }
    }

    private void DamagebleParams_OnParamChanged(DamagebleParam.ParamType paramType, float value, float maxValue)
    {
        if(paramType == DamagebleParam.ParamType.Health)
        {
            ChalkHealthFiil.fillAmount = value / maxValue;
        }
    }

    private void Player_OnAbilityTook(AAbility ability)
    {
        onAbilityTookText.text = ability.Description;
    }

    private void Player_OnAbilityNull()
    {
        onAbilityTookText.text = null;
        NullTextTween();
        
    }

    private void NullTextTween()
    {
        Vector3 punch = new Vector3(0f, 0f, 5f);

        var seq = DOTween.Sequence();
        seq.Append(onAbilityNullText.DOFade(1, 0.5f).From(0));
        seq.Join(onAbilityNullText.transform.DOBlendablePunchRotation(punch, 0.4f));
        seq.Append(onAbilityNullText.DOFade(0, 1));
    }


    #region CHALK

    private void GetShield(SpawnedObject obj)
    {
        shieldParamController = obj.gameObject.GetComponent<ShieldParamController>();
        if(shieldParamController == null)
        {
            shieldParamController = obj.gameObject.GetComponentInParent<ShieldParamController>();
            if(shieldParamController == null)
            {
                shieldParamController = obj.gameObject.GetComponentInChildren<ShieldParamController>();
            }
        }
    }
    

    private void ShieldDestroy()
    {
        Player_OnAbilityNull();
        StopAllCoroutines();
    }

    public void ChalkTimer()
    {
        StopAllCoroutines();
        StartCoroutine(ChalkDecrementTime(player.Ability.AbilityParams.ActiveTime));
    }

    private IEnumerator ChalkDecrementTime(float maxTime)
    {
        ChalkTimeFill.DOFade(1f, 2f).From(0.3f).SetLoops(-1, LoopType.Yoyo);


        var startTime = 0f;
        isMelReloading = true;
        for (float i = maxTime; 0f <= startTime; i -= Time.deltaTime)
        {
            startTime = i;
            ChalkTimeFill.fillAmount = i / maxTime;
            if (!isMelReloading)
            {
                startTime = 0f;
            }
            yield return new WaitForEndOfFrame();
        }
        isMelReloading = false;
    }

    #endregion
}
