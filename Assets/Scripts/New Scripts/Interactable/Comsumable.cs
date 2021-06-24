using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Comsumable : AInteractable
{
    [SerializeField] private float coolDownTime;

    [SerializeField] private UnityEvent OnUse;


    public override void Use()
    {
        OnUse?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<PlayerController>();

        if(player != null)
        {
            OnDetect?.Invoke();
            player.Interactable = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            OnUndetect?.Invoke();
            player.Interactable = null;
        }
    }
}
