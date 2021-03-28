using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SpawnedObject : MonoBehaviour
{
    [SerializeField] private int id;

    public delegate void OnDieEventHelper(GameObject gameObject);
    public event OnDieEventHelper OnDieEvent;

    public UnityEvent OnReincarnation;

    public int ID
    {
        get
        {
            return id;
        }
    }

    public void Die()
    {
        OnDieEvent?.Invoke(gameObject);
    }

    public void Reincornate()
    {
        OnReincarnation?.Invoke();
    }
}
