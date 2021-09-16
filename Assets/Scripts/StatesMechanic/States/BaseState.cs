using UnityEngine;

namespace CharacterStateMechanic
{
    public class BaseState : ACharacterState
    {
        private void Start()
        {
            StartState();
        }

        protected override void StartStateInternal()
        {
            Debug.Log("Базовый стейт врублен!");
        }

        protected override void StopStateInternal()
        {
            Debug.Log("Базовый стейт вырублен!");
        }
    }
}