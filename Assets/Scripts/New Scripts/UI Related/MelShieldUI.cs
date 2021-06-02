using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MelShieldUI : MonoBehaviour
{
    [SerializeField] private Image melFill;


    private bool isMelReloading = false;
    private PlayerController player;


    private void OnEnable()
    {
        player = PlayerInformation.GetInstance().PlayerController;
    }

    public void MelGlow()
    {
        melFill.DOFade(1f, 2f).From(0.3f).SetLoops(-1, LoopType.Yoyo);
    }

    public void MelDecrement()
    {
        StopAllCoroutines();
        StartCoroutine(MelDecrementTime(player.Ability1.AbilityParams.ActiveTime));
    }

    public void MelIncrement()
    {
        StopAllCoroutines();
        StartCoroutine(MelDecrementTime(player.Ability1.AbilityParams.CoolDownTime));
    }

    private IEnumerator MelIncrementTime(float maxTime)
    {
        isMelReloading = true;
        for (float i = 0; i < maxTime; i += Time.deltaTime)
        {
            var startTime = i;
            melFill.fillAmount = startTime / maxTime;
            if (!isMelReloading)
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
            if (!isMelReloading)
            {
                startTime = 0f;
            }
            yield return new WaitForEndOfFrame();
        }
        isMelReloading = false;
    }
}
