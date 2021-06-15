using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public enum PropState
{
    Serenity, Telekinesis
}

public class Prop : MonoBehaviour
{

    [SerializeField] private List<PropStateEvent> propEvent = new List<PropStateEvent>();

    private event Action<PropState> OnStateChanged;

    private PropState propState = PropState.Serenity;

    private void OnEnable()
    {
        OnStateChanged += Prop_OnStateChanged;
    }


    private void OnDisable()
    {
        OnStateChanged -= Prop_OnStateChanged;
    }

    public void ChangePropState(PropState newState)
    {
        propState = newState;
        OnStateChanged?.Invoke(propState);
    }

    private void Prop_OnStateChanged(PropState state)
    {
        for (int i = 0; i < propEvent.Count; i++)
        {
            if(propEvent[i].currentPropState == state)
            {
                StartCoroutine(propEvent[i].Invoke());
            }
        }
    }


}

[Serializable]
public class PropStateEvent
{
    public PropState currentPropState;

    [SerializeField] private UnityEvent OnNewStateSet;
    [SerializeField] private float timeToInvoke;

    public IEnumerator Invoke()
    {
        yield return new WaitForSeconds(timeToInvoke);
        OnNewStateSet?.Invoke();
    }
}
