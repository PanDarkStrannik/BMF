using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Comsumable : AInteractable
{

    [SerializeField] private UnityEvent OnUse;


    public override void Use()
    {
        if(State == InteractableState.Ready)
        {
            StartCoroutine(Reloading());
        }
    }

    #region PLAYER TRIGGERS

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

    #endregion

    protected override IEnumerator Reloading()
    {
        State = InteractableState.Reloading;
        if(state == InteractableState.Reloading)
        {
            OnUse?.Invoke();
            yield return new WaitForSeconds(1);
            StartCoroutine(Serenity(coolDownTime));
        }
    }

    protected override IEnumerator Serenity(float time)
    {
        yield return new WaitForSeconds(time);
        State = InteractableState.Ready;
    }
}
