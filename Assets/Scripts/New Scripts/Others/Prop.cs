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
    public bool isTaken = false;

    [SerializeField] private List<PropStateEvent> propEvent = new List<PropStateEvent>();

    private event Action<PropState> OnStateChanged;

    private PropState propState = PropState.Serenity;

    private void OnEnable()
    {
        OnStateChanged += Prop_OnStateChanged;
        OnStateChanged += OccupiedCheck;
    }


    private void OnDisable()
    {
        OnStateChanged -= Prop_OnStateChanged;
        OnStateChanged -= OccupiedCheck;

    }

    public void ChangePropState(PropState newState)
    {
        propState = newState;
        OnStateChanged?.Invoke(propState);
    }

    private void OccupiedCheck(PropState newState)
    {
        switch(newState)
        {
            case PropState.Serenity:
                isTaken = false;
                break;
            case PropState.Telekinesis:
                isTaken = true;
                break;
        }
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
