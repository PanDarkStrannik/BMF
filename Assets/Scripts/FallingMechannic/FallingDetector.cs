using StateMechanic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class FallingDetector : MonoBehaviour
{
    [SerializeField] private SphereCollider _groundCheckSphere;
    [SerializeField] private LayerMask _groundCheckMask;

    private float _groundedHeigth = 0f;
    private bool _grounded = false;
    private bool _faling = false;

    public event Action OnFallStart;
    public event Action<float> OnFallStop;

    private void Update()
    {
        if (isActiveAndEnabled)
            Falling();
    }

    private void Falling()
    {
        _grounded = Physics.CheckSphere(_groundCheckSphere.transform.position,
        _groundCheckSphere.radius, _groundCheckMask, QueryTriggerInteraction.Ignore);

        if (_grounded == true)
        {
            _groundedHeigth = _groundCheckSphere.transform.position.y;
            if(_faling == true)
            {
                _faling = false;
                var fallPos = _groundCheckSphere.transform.position.y;
                var heigth = _groundedHeigth - fallPos;
                heigth = Mathf.Abs(heigth);

                OnFallStop?.Invoke(heigth);
            }
        }
        else if(_faling == false)
        {
            _faling = true;
            OnFallStart?.Invoke();
        }
    }
}
