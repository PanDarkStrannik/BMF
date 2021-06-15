using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Prop : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private List<PropVelocityEvent> propEvents = new List<PropVelocityEvent>();

    private void FixedUpdate()
    {
        OnVelocityChanged();
    }

    private void OnVelocityChanged()
    {
        for (int i = 0; i < propEvents.Count; i++)
        {
            if(propEvents[i].Velocity == body.velocity.sqrMagnitude)
            {
                propEvents[i].Invoke();
            }
        }
    }
}

[System.Serializable]
public class PropVelocityEvent
{
    [SerializeField] private float velocity;
    [SerializeField] private float timeToInvoke;
    [SerializeField] private UnityEvent OnVelocityChanged;

    #region PROPERITES
    public float Velocity { get => velocity; }

    #endregion

    public IEnumerator Invoke()
    {
        yield return new WaitForSeconds(timeToInvoke);
        OnVelocityChanged?.Invoke();
    }
}
