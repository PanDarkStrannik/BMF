using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class PlayerParamController : ParamController
{
    [SerializeField] protected PlayerUI playerUI;
    [SerializeField] protected Rigidbody playerBody;
    [SerializeField] protected List<GameObject> deactiveObjects;
    [SerializeField] private Vector3 torgueForceOnDeath;

   // [SerializeField] private UnityEvent PlayerDamagedEvent;

    [SerializeField] private ShakingParams shakingParams;



    public ShakingParams ShakingParams
    {
        get
        {
            return shakingParams;
        }
    }

    public float MaxHealth
    {
        get; private set;
    }


    private bool isFirstCheck = true;

    //public delegate void OnPlayerChangedParamHelper(float damageValue, DamagebleParam.ParamType paramType);
    //public event OnPlayerChangedParamHelper PlayerDamaged;


    protected override void OnEnable()
    {
        base.OnEnable();
        paramSum.OnParamsDamaged += OnParamsDamagedListener;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        paramSum.OnParamsDamaged -= OnParamsDamagedListener;
    }

    private PlayerParamController()
    {
        PlayerInformation.GetInstance().PlayerParamController = this;
    }
  

    private void Start()
    {
        //PlayerInformation.GetInstance().PlayerMovement.FallingEvent += FallingDamage;
    }

    private void OnParamsDamagedListener()
    {
        shakingParams.ShakeEventInvoke();
    }




    protected override void CheckTypeAndValues(DamagebleParam.ParamType type, float value, float maxValue)
    {      
        switch (type)
        {
            case DamagebleParam.ParamType.Health:
                if (isFirstCheck)
                {
                    //playerUI.InitializePlayerView(maxValue);
                    //PlayerDamaged?.Invoke(maxValue);
                    MaxHealth = maxValue;
                    isFirstCheck = false;
                 //   PlayerDamaged?.Invoke(value);
                    //PlayerDamagedEvent?.Invoke();
                }
                else
                {
                 //   PlayerDamaged?.Invoke(value);
                  //  PlayerDamagedEvent?.Invoke();
                  //  shakingParams.ShakeEventInvoke();
                }
               // playerUI.ViewHealth(value);
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
