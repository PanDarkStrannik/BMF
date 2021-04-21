using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCameraShaking : MonoBehaviour
{
    [SerializeField] private PlayerParamController paramController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Camera playerCamera;

    private Shaker shaker;
    private bool shakinAlready=false;

    private void Start()
    {
        shaker = new Shaker();
        //paramController.PlayerDamaged += Shaking;
        paramController.ShakingParams.ShakeEvent += Shaking;
        if (playerController != null)
        {
            foreach (var e in playerController.GunPushes)
            {
                e.ShakingParams.ShakeEvent += Shaking;
            }
        }

    }


    private void Shaking(float duration, float strength)
    {
        if (shakinAlready == false)
        {
            shakinAlready = true;
            shaker.ShakePosition(playerCamera.transform, duration, strength);
            StartCoroutine(ReturnToMainPos(duration));
        }
    }

    private IEnumerator ReturnToMainPos(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        shakinAlready = false;
    }

    private void OnDestroy()
    {
        //paramController.PlayerDamaged -= Shaking;
        foreach (var e in playerController.GunPushes)
        {
            e.ShakingParams.ShakeEvent -= Shaking;
        }
        shaker.KillAllTweens();
    }
}
