using Scripts.DevelopingSupporting;
using System.Collections.Generic;
using UnityEngine;
using PushMechannic;
using System;

namespace AttackModifier
{
    public class ImpulseModForAttack : AAttackModifier, IPusher 
    {
        [SerializeField] private Transform _forceCenter;
        [SerializeField] private float _forceValue;
        [SerializeField] private ForceMode _forceMode;

        public event Action<IPusher> OnPushingComplete;

        public override void AttackByModifier(Collider objectForAttack)
        {
            if (isActiveAndEnabled == true)
            {
                if (SupportingScripts.FindHelper.
                    TryFindAllComponentsFromIerarhy(objectForAttack.gameObject, out List<IPushable> pushables) == true)
                {
                    foreach (var pushable in pushables)
                        pushable.TryPush(this);
                    //if (objectForAttack.TryGetComponent(out Rigidbody objectRigidbody) == true)
                    //{
                    //Debug.Log("Риджид боди найден!");
                    //foreach(var rigidBody in objectsRigidbody)
                    //{
                    //    Vector3 directionFromCenter = rigidBody.transform.position - _forceCenter.position;
                    //    rigidBody.AddForce(directionFromCenter * _forceValue, _forceMode);
                    //}
                    //}
                }
            }
        }

        public void Push(List<Rigidbody> rigidbodies)
        {
            foreach (var rigidBody in rigidbodies)
            {
                Vector3 directionFromCenter = rigidBody.transform.position - _forceCenter.position;
                rigidBody.AddForce(directionFromCenter * _forceValue, _forceMode);
            }     
        }


        public override void OnDisable()
        {
            Debug.Log($"Модуль {typeof(DamageModForAttack).Name} на {gameObject.name} отключен!");
            OnPushingComplete?.Invoke(this);
        }

        public override void OnEnable()
        {
            Debug.Log($"Модуль {typeof(DamageModForAttack).Name} на {gameObject.name} активирован!");
        }
    }
}