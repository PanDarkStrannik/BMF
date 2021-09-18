
namespace StateMechanic
{
    public sealed class TranzitionFromStopedStates : AStateTranzition
    {
        private void OnEnable()
        {
            foreach(var state in FromStates)
                state.OnStateStoped += State_OnStateStoped;
        }

        private void OnDisable()
        {
            foreach (var state in FromStates)
                state.OnStateStoped -= State_OnStateStoped;
        }

        private void State_OnStateStoped(IState stoppedState)
        {
            
            if (FromStates.TrueForAll(x => x.IsStateActive == false))
            {
                foreach(var state in ToStates)
                    state.StartState(this);
            }
        }

        protected override void StartStateInternal(IState state)
        {
            state.StartState();
        }
    }
}