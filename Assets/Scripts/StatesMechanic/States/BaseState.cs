using UnityEngine;

namespace StateMechanic
{
    public class BaseState : AState
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