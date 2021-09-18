using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMechanic
{
    public interface IStateStarter
    {
        bool TryStartState(IState state);
    }
}