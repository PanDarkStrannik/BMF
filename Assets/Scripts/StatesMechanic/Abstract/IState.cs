using System;
using System.Collections.Generic;

namespace StateMechanic
{
    public interface IState
    {
        event Action<IState> OnStateStarted;
        event Action<IState> OnStateStoped;
        event Action<IState> OnStateStartFailed;
        List<IStateConnection> StateConnections
        { get; }
        bool IsStateActive
        { get; }
        void StartState();
        void StartState(IStateStarter stateStarter);
        void StopState();
    }
}