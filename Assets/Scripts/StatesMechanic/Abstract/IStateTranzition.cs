using System.Collections.Generic;

namespace StateMechanic
{
    public interface IStateTranzition : IStateStarter
    {
        List<IState> FromStates
        { get; }
        List<IState> ToStates
        { get; }
    }
}