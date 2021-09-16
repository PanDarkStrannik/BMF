using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateMechanic
{
    public class AController : MonoBehaviour
    {
       [SerializeField] private List<ACharacterState> _statesWantsDisableUs = new List<ACharacterState>();
       [SerializeField] private List<ACharacterState> _statesWantsEnableUs = new List<ACharacterState>();

        public bool TryEnable(ACharacterState stateToEnable)
        {
            AddState(stateToEnable, _statesWantsEnableUs);
            if (TryChangeActive(true))
                return true;
            return false;
        }


        public bool TryDisable(ACharacterState stateToDisable)
        {
            AddState(stateToDisable, _statesWantsDisableUs);
            if (TryChangeActive(false))
                return true;
            return false;
        }

        public void RemoveState(ACharacterState characterState)
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

                List<ACharacterState> controllingStates;
                if (isActive == true)
                {
                    controllingStates = new List<ACharacterState>(_statesWantsDisableUs);
                }
                else
                {
                    controllingStates = new List<ACharacterState>(_statesWantsEnableUs);
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

        private void AddState(ACharacterState characterState, List<ACharacterState> characterStates)
        {
            if (characterStates.Contains(characterState) == false)
                characterStates.Add(characterState);
        }
    }
}