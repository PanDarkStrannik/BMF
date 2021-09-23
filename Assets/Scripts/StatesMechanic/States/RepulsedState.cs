using System.Collections.Generic;
using UnityEngine;
using PushMechannic;

namespace StateMechanic
{
    public class RepulsedState : AState, IPushable
    {
        [SerializeField] private List<Rigidbody> _rigidbodysForPush;

        private Dictionary<Rigidbody, bool> _rigidbodyIsKinematicCompaerer = new Dictionary<Rigidbody, bool>();

        public bool TryPush(IPusher pusher)
        {
            if(TryStartStateByConnections())
            {
                pusher.OnPushingComplete += OnPushingComplete;
                pusher.Push(_rigidbodysForPush);
                return true;
            }
            return false;
        }

        private void OnPushingComplete(IPusher pusher)
        {
            pusher.OnPushingComplete -= OnPushingComplete;
            foreach (var rigidbody in _rigidbodysForPush)
                rigidbody.velocity = Vector3.zero;
            StopState();
        }

        protected override void StartStateInternal()
        {
            foreach (var rigidbody in _rigidbodysForPush)
            {
                _rigidbodyIsKinematicCompaerer.Add(rigidbody, rigidbody.isKinematic);
                rigidbody.isKinematic = false;
            }
            Debug.Log($"Стейт {typeof(RepulsedState).Name} на {gameObject.name} стартовал!");
        }

        protected override void StopStateInternal()
        {
            foreach (var rigidbody in _rigidbodysForPush)
            {
                rigidbody.isKinematic = _rigidbodyIsKinematicCompaerer[rigidbody];
            }
            _rigidbodyIsKinematicCompaerer.Clear();
            Debug.Log($"Стейт {typeof(RepulsedState).Name} на {gameObject.name} остановился!");
        }
    }
}