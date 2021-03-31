using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class AInteractable : MonoBehaviour
{
    [SerializeField] protected string description;


    public UnityEvent OnDetect;
    public UnityEvent OnUndetect;

   
    public string GetDescription()
    {
        return description;
    }


    // не работает 
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


}
