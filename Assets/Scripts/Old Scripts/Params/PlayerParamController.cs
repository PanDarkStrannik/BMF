using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerParamController : ParamController
{
    [SerializeField] protected PlayerUI playerUI;
    [SerializeField] protected Rigidbody playerBody;
    [SerializeField] protected List<GameObject> deactiveObjects;
    [SerializeField] private Vector3 torgueForceOnDeath;

    [SerializeField] private UnityEvent PlayerDamagedEvent;

    [SerializeField] private ShakingParams shakingParams;

    public ShakingParams ShakingParams
    {
        get
        {
            return shakingParams;
        }
    }

    public delegate void PlayerDamagedHelper();
    public event PlayerDamagedHelper PlayerDamaged;

    private bool isFirstCheck = true;

    protected override void CheckTypeAndValues(DamagebleParam.ParamType type, float value, float maxValue)
    {      
        switch (type)
        {
            case DamagebleParam.ParamType.Health:
                if (isFirstCheck)
                {
                    playerUI.InitializePlayerView(maxValue);
                    isFirstCheck = false;
                }
                else
                {
                    PlayerDamaged?.Invoke();
                    PlayerDamagedEvent?.Invoke();
                    shakingParams.ShakeEventInvoke();
                }
                playerUI.ViewHealth(value);
                break;
        }
    }

    protected override IEnumerator NullHealth()
    {
        foreach(var e in deactiveScripts)
        {
            e.enabled = false;
        }
        playerBody.freezeRotation = false;
        playerBody.AddTorque(torgueForceOnDeath, ForceMode.Impulse);
        yield return new WaitForSeconds(timeToDeactive);
        foreach(var e in deactiveObjects)
        {
            e.SetActive(false);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
