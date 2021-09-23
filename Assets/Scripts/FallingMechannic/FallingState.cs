using System.Collections.Generic;
using UnityEngine;

namespace StateMechanic
{
    public class FallingState : AState
    {
        [SerializeField] private Ragdoll _ragdoll;
        [SerializeField] private FallingDetector _fallingDetector;

        private Dictionary<Rigidbody, bool> _rigidbodyIsKinematicComparers = new Dictionary<Rigidbody, bool>();
        private Dictionary<Collider, bool> _colliderIsDeactiveComparers = new Dictionary<Collider, bool>();

        private void OnEnable()
        {
            _fallingDetector.OnFallStart += OnFallStart;
            _fallingDetector.OnFallStop += OnFallStop;
        }

        private void OnFallStart()
        {
            TryStartStateByConnections();
        }

        private void OnFallStop(float obj)
        {
            StopState();
        }

        private void OnDisable()
        {
            _fallingDetector.OnFallStart -= OnFallStart;
            _fallingDetector.OnFallStop -= OnFallStop;
        }

        protected override void StartStateInternal()
        {

            if (_ragdoll != null)
                _ragdoll.ChangeRagdoll(false);
        }

        protected override void StopStateInternal()
        {
            if (_ragdoll != null)
                _ragdoll.ChangeRagdoll(true);
            _colliderIsDeactiveComparers.Clear();
            _rigidbodyIsKinematicComparers.Clear();
        }

    }
}