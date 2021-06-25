using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public enum InteractableState
{
    Ready,
    Reloading
}

public abstract class AInteractable : MonoBehaviour
{

    [SerializeField] protected string description;
    [SerializeField, Min(0)] protected float coolDownTime;

    [SerializeField] private List<InteractableStateEvent> interactableStateEvent = new List<InteractableStateEvent>();

    protected InteractableState state;


    #region EVENTS

    public UnityEvent OnDetect;
    public UnityEvent OnUndetect;

    private event Action<InteractableState> OnStateChanged;

    #endregion

    #region PROPERTIES

    public InteractableState State
    {
        get => state;
        set
        {
            state = value;
            OnStateChanged?.Invoke(value);
        }
    }

    public float CoolDownTime { get => coolDownTime; }

    #endregion

    private void OnEnable()
    {
        OnStateChanged += AInteractable_OnStateChanged;
    }

    private void OnDisable()
    {
        OnStateChanged -= AInteractable_OnStateChanged;
    }

    private void AInteractable_OnStateChanged(InteractableState newState)
    {
        for (int i = 0;  i < interactableStateEvent.Count; i++)
        {
            if(interactableStateEvent[i].CurrentState == newState)
            {
                StartCoroutine(interactableStateEvent[i].Invoke());
            }
        }
    }

    public string GetDescription()
    {
        return description;
    }


    public virtual void Unsubsribe(List<UnityAction> action)
    {
        foreach (var e in action)
        {
            OnDetect.RemoveListener(e);
            OnUndetect.RemoveListener(e);
        } 
    }

    public virtual void Unsubscribe()
    {
        OnDetect.RemoveAllListeners();
        OnUndetect.RemoveAllListeners();
    }   

    public abstract void Use();


    protected virtual IEnumerator Reloading()
    {
        State = InteractableState.Reloading;
        if(state == InteractableState.Reloading)
        {
            yield return new WaitForSeconds(1);
        }

    }

    protected virtual IEnumerator Serenity(float time)
    {
        yield return new WaitForSeconds(time);
        State = InteractableState.Ready;
    }

}

[Serializable]
public class InteractableStateEvent
{
    [SerializeField] private InteractableState currentState;
    [SerializeField] private float timeToInvoke;
    [SerializeField] private UnityEvent OnStateEnter;

    public InteractableState CurrentState { get => currentState; }

    public IEnumerator Invoke()
    {
        yield return new WaitForSeconds(timeToInvoke);
        OnStateEnter?.Invoke();
    }
}