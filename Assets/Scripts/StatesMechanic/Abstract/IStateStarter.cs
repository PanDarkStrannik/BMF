using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateMechanic
{
    public interface IStateStarter
    {
        bool TryStartState(IState state);
    }
}