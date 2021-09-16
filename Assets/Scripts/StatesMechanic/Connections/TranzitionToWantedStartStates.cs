
using UnityEngine;

namespace CharacterStateMechanic
{
    public sealed class TranzitionToWantedStartStates : AStateTranzition
    {
        private IState _stateToTranzit = null;

        private void OnEnable()
        {
            foreach (var state in ToStates)
                state.OnStateStartFailed += OnStateStartedFailed;
        }

        private void OnDisable()
        {
            foreach (var state in ToStates)
                state.OnStateStartFailed -= OnStateStartedFailed;
        }

        private void OnStateStartedFailed(IState startFailedState)
        {
            foreach (var state in FromStates)
            {
                if (state.IsStateActive == true)
                    state.StopState();
            }
            startFailedState.StartState(this);
        }


        protected override void StartStateInternal(IState state)
        {
            Debug.Log($"Должен произойти внутренний переход у транзитора {name}, объект для перехода: {_stateToTranzit}");
            state.StartState();
        }
    }
}