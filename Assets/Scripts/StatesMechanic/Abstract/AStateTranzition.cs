using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateMechanic
{
    public abstract class AStateTranzition : MonoBehaviour, IStateTranzition
    {
        [SerializeField] private List<ACharacterState> _fromStates;
        [SerializeField] private List<ACharacterState> _toStates;

        public List<IState> FromStates
        { get; private set; }

        public List<IState> ToStates
        { get; private set; }

        public void Awake()
        {
            FromStates = new List<IState>(_fromStates);
            ToStates = new List<IState>(_toStates);
        }

        //public void Tranzit()
        //{
        //    TranzitInternal();
        //}

        public bool TryStartState(IState state)
        {
            StartStateInternal(state);
            return true;
        }

        protected abstract void StartStateInternal(IState state);
    }
}