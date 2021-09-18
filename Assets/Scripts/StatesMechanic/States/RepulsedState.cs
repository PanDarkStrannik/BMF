using System.Collections.Generic;
using UnityEngine;
using PushMechannic;

namespace StateMechanic
{
    public class RepulsedState : ACharacterState, IPushable
    {
        [SerializeField] private List<Rigidbody> _rigidbodysForPush;

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
                rigidbody.Sleep();
            StopState();
        }

        protected override void StartStateInternal()
        {
            Debug.Log($"����� {typeof(RepulsedState).Name} �� {gameObject.name} ���������!");
        }

        protected override void StopStateInternal()
        {
            Debug.Log($"����� {typeof(RepulsedState).Name} �� {gameObject.name} �����������!");
        }
    }
}