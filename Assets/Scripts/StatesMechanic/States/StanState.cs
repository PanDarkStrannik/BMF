using System.Collections;
using UnityEngine;
using StanMechannic;

namespace StateMechanic
{
    public class StanState : ACharacterState, IStanableObject
    {
        [SerializeField, Min(0f)] private float _stanTime=1f;

        private IStanData _stanData=null;

        private Coroutine _stanCoroutine = null;

        public void StanObject(IStanData stanData)
        {
            _stanData = stanData;
            TryStartStateByConnections();
        }

        protected override void StartStateInternal()
        {
            Debug.Log("Стан стартовал!");
            if (_stanCoroutine == null)
                _stanCoroutine = StartCoroutine(StanCorutine());
        }

        private IEnumerator StanCorutine()
        {
            if (_stanData != null)
                yield return new WaitForSeconds(_stanData.StanTime);
            _stanData = null;
            StopState();
        }

        protected override void StopStateInternal()
        {
            if (_stanCoroutine != null)
                StopCoroutine(_stanCoroutine);
            _stanCoroutine = null;
            Debug.Log("Стан окончился!");
        }
    }
}