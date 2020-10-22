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

    [SerializeField] private UnityEvent PlayerDamagedEvent;

    [SerializeField] private ShakingParams shakingParams;

    [SerializeField] private List<HeigthAndDamage> heigthsAndDamages;


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

    private void Awake()
    {
        heigthsAndDamages.OrderBy(x => x.Heigth);
        heigthsAndDamages.Reverse();
        PlayerInformation.GetInstance().PlayerParamController = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerInformation.GetInstance().PlayerMovement.FallingEvent += FallingDamage;
    }

    protected override void OnDisable()
    {
        PlayerInformation.GetInstance().PlayerMovement.FallingEvent -= FallingDamage;
        base.OnDisable();
    }

    protected void FallingDamage(float heigth)
    {
        for (int i = 0; i < heigthsAndDamages.Count; i++)
        {
            if (heigth >= heigthsAndDamages[i].Heigth)
            {
                paramSum.DamageAllByType(heigthsAndDamages[i].Damage);
                Debug.Log("Урон от падения: " + heigthsAndDamages[i].Damage.Value);
                break;
            }
        }
    }


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


    [System.Serializable]
    public class HeigthAndDamage
    {
        [SerializeField] private float heigth = 0f;
        [SerializeField] private DamageByType damage;

        public float Heigth
        {
            get
            {
                return heigth;
            }
        }

        public DamageByType Damage
        {
            get
            {
                return damage;
            }
        }

    }

}
