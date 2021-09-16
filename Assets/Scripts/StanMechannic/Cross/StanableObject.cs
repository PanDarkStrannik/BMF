using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StanableObject : MonoBehaviour, IStanable
{
    [SerializeField] private UnityEvent _onStanStart;
    [SerializeField] private UnityEvent _onStanEnd; 

    public bool CanStan
    { get; private set; }

    public event Action<IStanable> OnStanStart;
    public event Action<IStanable> OnStanEnd;

    private void Awake()
    {
        CanStan = false;
    }

    public void Stan(StanData stanData)
    {
        if (CanStan == false)
            StartCoroutine(StanCoroutine(stanData));
    }

    public IEnumerator StanCoroutine(StanData stanData)
    {
        Debug.Log($"Стан у {gameObject.name} начался!");
        CanStan = true;
        OnStanStart?.Invoke(this);
        _onStanStart?.Invoke();
        yield return new WaitForSeconds(stanData.StanTime);
        CanStan = false;
        OnStanEnd?.Invoke(this);
        _onStanEnd?.Invoke();
    }
}
