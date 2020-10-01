using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraShaking : MonoBehaviour
{
    [SerializeField] private PlayerParamController paramController;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float strength = 1f;

    private Shaker shaker;


    private void Start()
    {
        shaker = new Shaker();
        paramController.PlayerDamaged += Shaking;
    }

    private void Shaking()
    {

        shaker.ShakePosition(playerCamera.transform, duration, strength);

    }

    private void OnDestroy()
    {
        paramController.PlayerDamaged -= Shaking;
        shaker.KillAllTweens();
    }
}
