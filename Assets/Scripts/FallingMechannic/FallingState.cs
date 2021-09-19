using System.Collections.Generic;
using UnityEngine;

namespace StateMechanic
{
    public class FallingState : AState
    {
        [SerializeField] private List<Rigidbody> _rigidbodysToDeactive;
        [SerializeField] private List<Collider> _collidersToDeactive;
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
            foreach (var rigidbody in _rigidbodysToDeactive)
            {
                _rigidbodyIsKinematicComparers.Add(rigidbody, rigidbody.isKinematic);
                rigidbody.isKinematic = true;
            }

            foreach (var collider in _collidersToDeactive)
            {
                _colliderIsDeactiveComparers.Add(collider, collider.enabled);
                collider.enabled = false;
            }

            if (_ragdoll != null)
                _ragdoll.ChangeRagdoll(false);
        }

        protected override void StopStateInternal()
        {
            if (_ragdoll != null)
                _ragdoll.ChangeRagdoll(true);
            foreach (var rigidbody in _rigidbodysToDeactive)
            {
                rigidbody.isKinematic = _rigidbodyIsKinematicComparers[rigidbody];
            }
            foreach (var collider in _collidersToDeactive)
            {
                collider.enabled = _colliderIsDeactiveComparers[collider];
            }
            _colliderIsDeactiveComparers.Clear();
            _rigidbodyIsKinematicComparers.Clear();
        }

    }
}