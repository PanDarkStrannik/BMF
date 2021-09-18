using System.Collections.Generic;
using UnityEngine;

namespace StateMechanic
{
    public sealed class StateConnection : MonoBehaviour, IStateConnection
    {
        [SerializeField] private List<ACharacterState> _connectedStates;
        [SerializeField] private List<StateConnection> _connectionsForBan;
        [SerializeField] private bool _isConnectionReady = false;
        [SerializeField] private bool _isConnectionBanned = false;
        public bool IsConnectionReady
        {
            get
            {
                return _isConnectionReady;
            }
            private set
            {
                _isConnectionReady = value;
                foreach (var connection in _connectionsForBan)
                    connection.ChangeBanValue(this);
            }
        }
        public bool IsConnectionBanned
        {
            get
            {
                return _isConnectionBanned;
            }
            private set
            {
                _isConnectionBanned = value;
                foreach (var connection in _connectionsForBan)
                    connection.ChangeBanValue(this);
            }
        }

        private void Awake()
        {
            foreach (var state in _connectedStates)
                state.AddConnection(this);
        }

        private void OnEnable()
        {
            foreach (var state in _connectedStates)
            {
                state.OnStateStarted += ChangeConnectionReady;
                state.OnStateStoped += ChangeConnectionReady;
            }
        }

        private void OnDisable()
        {
            foreach (var state in _connectedStates)
            {
                state.OnStateStarted -= ChangeConnectionReady;
                state.OnStateStoped -= ChangeConnectionReady;
            }
        }

        private void ChangeConnectionReady(IState characterState)
        {
            IsConnectionReady = !_connectedStates.TrueForAll(x => x.IsStateActive == false);
        }

        public void ChangeBanValue(IStateConnection reasonForChange)
        {
            if (reasonForChange.IsConnectionBanned == false)
                IsConnectionBanned = !reasonForChange.IsConnectionReady;
            else if (IsConnectionBanned == true)
                IsConnectionBanned = false;
        }
    }
}