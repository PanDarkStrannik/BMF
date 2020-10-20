using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraShaking : MonoBehaviour
{
    [SerializeField] private PlayerParamController paramController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Camera playerCamera;

    private Shaker shaker;


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

    //private void Shaking()
    //{

    //    shaker.ShakePosition(playerCamera.transform, duration, strength);

    //}

    private void Shaking(float duration, float strength)
    {
        shaker.ShakePosition(playerCamera.transform, duration, strength);
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
