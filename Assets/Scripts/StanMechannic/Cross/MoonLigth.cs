using System.Collections.Generic;
using UnityEngine;
using Scripts.DevelopingSupporting;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class MoonLigth : MonoBehaviour
{
    [SerializeField] private Color _moonLigthColor = Color.cyan;
    [SerializeField] private List<MoonLigthUsesEvents> _moonLigthUsesEvents;

    private int _counter = 0;

    private Rigidbody _rigidbody;
    private Collider _colider;

    private List<IMoonLigthUser> _moonLigthUsers = new List<IMoonLigthUser>();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colider = GetComponent<Collider>();

        _rigidbody.isKinematic = true;
        _colider.isTrigger = true;
    }

    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colider = GetComponent<Collider>();

        _rigidbody.isKinematic = true;
        _colider.isTrigger = true;
    }

    private void OnDisable()
    {
        foreach(var moonLigthUser in _moonLigthUsers)
        {
            moonLigthUser.OnMoonLigthExit();
            moonLigthUser.OnMoonLigthUsed -= MoonLigthUser_OnMoonLigthUsed;
        }
        _moonLigthUsers.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != null)
        {
            if(SupportingScripts.FindHelper.SearchInIerarhiy(other.gameObject, out IMoonLigthUser moonLigthUser))
            {
                if (_moonLigthUsers.Contains(moonLigthUser) == false)
                {
                    moonLigthUser.OnMoonLigthUsed += MoonLigthUser_OnMoonLigthUsed;
                    moonLigthUser.OnMoonLigthEnter();
                    _moonLigthUsers.Add(moonLigthUser);
                } 
            }
        }
    }

    private void MoonLigthUser_OnMoonLigthUsed()
    {
        _counter++;
        foreach(var moonLigthEvent in _moonLigthUsesEvents)
        {
            moonLigthEvent.TryInvoke(_counter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null)
        {
            if (SupportingScripts.FindHelper.SearchInIerarhiy(other.gameObject, out IMoonLigthUser moonLigthUser))
            {
                if (_moonLigthUsers.Contains(moonLigthUser) == true)
                {
                    moonLigthUser.OnMoonLigthExit();
                    moonLigthUser.OnMoonLigthUsed -= MoonLigthUser_OnMoonLigthUsed;
                    _moonLigthUsers.Remove(moonLigthUser);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _moonLigthColor;
        var center = transform.position + transform.InverseTransformPoint(_colider.bounds.center);
        var size = transform.InverseTransformVector(_colider.bounds.size);
        Gizmos.DrawCube(center, size);
    }

    [System.Serializable]
    private class MoonLigthUsesEvents
    {
        [SerializeField,Min(0)] private int _usesCount = 0;
        [SerializeField] private bool _invokeAtOnce = false;
        [SerializeField] private UnityEvent<int> _onUsesEvent;

        private bool _alreadyInvoke = false;

        public bool TryInvoke(int currentCount)
        {
            if (_invokeAtOnce == true && _alreadyInvoke == true)
                return false;
            if(_usesCount == currentCount)
            {
                _alreadyInvoke = true;
                _onUsesEvent?.Invoke(_usesCount);
            }
            return false;
        }
    }
}
