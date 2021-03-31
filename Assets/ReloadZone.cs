﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class ReloadZone : AInteractable
{
    public UnityEvent OnReloadComplete;
    public UnityEvent OnReloadStart;
    public UnityEvent OnReloadFailed;
    public UnityEvent OnReloading;

    


    private bool isReload = false;
    private bool isInRange = false;

    private Coroutine reloadCoroutine = null;

    [SerializeField, Min (0)]
    private float reloadTime;

    

    #region PROPERTIES

    public float CurrentReloadTime { get; private set; }
    public float ReloadTime { get => reloadTime; }


    #endregion


    private void Start()
    {
        OnReloadComplete.AddListener(AddWater);
    }


    private void AddWater()
    {
        var damagebleParam = PlayerInformation.GetInstance().PlayerParamController.DamagebleParams;
        damagebleParam.ChangeParam(DamagebleParam.ParamType.HolyWater, damagebleParam.typesMaxValues[DamagebleParam.ParamType.HolyWater]);
    }


    public override void Use()
    {
        if (isInRange && reloadCoroutine == null)
        {
          reloadCoroutine = StartCoroutine(Reload());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if(player != null)
        {
            OnDetect?.Invoke();
            isInRange = true;
            player.Interactable = this;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            OnUndetect?.Invoke();
            isReload = false;
            isInRange = false;
            player.Interactable = null;
        }
    }


    public override void Unsubsribe(List<UnityAction> actions)
    {
        base.Unsubsribe(actions);
        foreach (var e in actions)
        {
            OnReloadComplete.RemoveListener(e);
            OnReloading.RemoveListener(e);
            OnReloadFailed.RemoveListener(e);
        }
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();
        OnReloadComplete.RemoveAllListeners();
        OnReloading.RemoveAllListeners();
        OnReloadFailed.RemoveAllListeners();
    }

    private IEnumerator Reload()
    {
        OnReloadStart?.Invoke();
        isReload = true;

        for (float i = 0; i < reloadTime; i += Time.deltaTime)
        {
            CurrentReloadTime = i;
            OnReloading?.Invoke();
            if(!isReload)
            {
                CurrentReloadTime = 0f;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        if(isReload)
        {
          OnReloadComplete?.Invoke();
        }
        else
        {
            OnReloadFailed?.Invoke();
        }
        isReload = false;
        reloadCoroutine = null;
    }

}
