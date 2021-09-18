using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMechanic
{
    public class StateStarterByConnections : IStateStarter
    {
        public bool TryStartState(IState state)
        {
            if (state.StateConnections != null)
            {
                if (!state.StateConnections.TrueForAll(
                    x => x.IsConnectionReady == false
                || x.IsConnectionBanned == true))
                {
                    state.StartState();
                    return true;
                }
            }
            return false;
        }
    }
}