using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMechanic
{
    public abstract class AState : MonoBehaviour, IState
    {
        [SerializeField] private List<AController> _controllersToEnable;
        [SerializeField] private List<AController> _controllersToDisable;

        private StateStarterByConnections _stateStarterByConnections = new StateStarterByConnections();

        public event Action<IState> OnStateStarted;
        public event Action<IState> OnStateStoped;
        public event Action<IState> OnStateStartFailed;

        public bool IsStateActive
        { get; protected set; }

        public List<IStateConnection> StateConnections
        { get; private set; }

        public void AddConnection(IStateConnection stateConnection)
        {
            if (StateConnections == null)
                StateConnections = new List<IStateConnection>();
            StateConnections.Add(stateConnection);
        }

        [ContextMenu("StartStateByConnections()")]
        public bool TryStartStateByConnections()
        {
            if(_stateStarterByConnections.TryStartState(this) == false)
            {
                OnStateStartFailed?.Invoke(this);
                return false;
            }
            return true;
        }

        public void StartState(IStateStarter stateStarter)
        {
            stateStarter.TryStartState(this);
        }

        public void StartState()
        {
            if (IsStateActive == false)
            {
                IsStateActive = true;
                foreach (var controllerToEnable in _controllersToEnable)
                    controllerToEnable.TryEnable(this);
                foreach (var controllerToDisable in _controllersToDisable)
                    controllerToDisable.TryDisable(this);

                StartStateInternal();
                OnStateStarted?.Invoke(this);
            }
        }



        [ContextMenu("StopState()")]
        public void StopState()
        {
            if (IsStateActive == true)
            {
                IsStateActive = false;
                foreach (var controllerToEnable in _controllersToEnable)
                    controllerToEnable.RemoveState(this);
                foreach (var controllerToDisable in _controllersToDisable)
                    controllerToDisable.RemoveState(this);

                StopStateInternal();
                OnStateStoped?.Invoke(this);
            }
        }
        protected abstract void StartStateInternal();
        protected abstract void StopStateInternal();
    }
}