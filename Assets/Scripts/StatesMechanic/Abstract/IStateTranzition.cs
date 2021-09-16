using System.Collections.Generic;

namespace CharacterStateMechanic
{
    public interface IStateTranzition : IStateStarter
    {
        List<IState> FromStates
        { get; }
        List<IState> ToStates
        { get; }
    }
}