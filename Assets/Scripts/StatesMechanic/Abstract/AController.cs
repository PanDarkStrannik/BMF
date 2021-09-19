using System.Collections.Generic;
using UnityEngine;

namespace StateMechanic
{
    public class AController : MonoBehaviour
    {
       [SerializeField] private List<AState> _statesWantsDisableUs = new List<AState>();
       [SerializeField] private List<AState> _statesWantsEnableUs = new List<AState>();

        public bool TryEnable(AState stateToEnable)
        {
            AddState(stateToEnable, _statesWantsEnableUs);
            if (TryChangeActive(true))
                return true;
            return false;
        }


        public bool TryDisable(AState stateToDisable)
        {
            AddState(stateToDisable, _statesWantsDisableUs);
            if (TryChangeActive(false))
                return true;
            return false;
        }

        public void RemoveState(AState characterState)
        {
            if (_statesWantsEnableUs.Contains(characterState) == true)
            {
                _statesWantsEnableUs.Remove(characterState);
                TryChangeActive(false);
            }
            if (_statesWantsDisableUs.Contains(characterState) == true)
            {
                _statesWantsDisableUs.Remove(characterState);
                TryChangeActive(true);
            }
        }

        private bool TryChangeActive(bool isActive)
        {
            if (_statesWantsDisableUs.Count != 0 || _statesWantsEnableUs.Count != 0)
            {

                List<AState> controllingStates;
                if (isActive == true)
                {
                    controllingStates = new List<AState>(_statesWantsDisableUs);
                }
                else
                {
                    controllingStates = new List<AState>(_statesWantsEnableUs);
                }

                if (enabled == !isActive)
                {
                    if (controllingStates.TrueForAll(state => state.IsStateActive == false))
                    {
                        enabled = isActive;
                        return true;
                    }
                }
            }
            return false;
        }

        private void AddState(AState characterState, List<AState> characterStates)
        {
            if (characterStates.Contains(characterState) == false)
                characterStates.Add(characterState);
        }
    }
}