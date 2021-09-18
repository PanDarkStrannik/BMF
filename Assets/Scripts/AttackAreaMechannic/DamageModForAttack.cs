using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.DevelopingSupporting;

namespace AttackModifier
{
    public class DamageModForAttack : AAttackModifier
    {
        [SerializeField, Min(0f)] private float _timeBetweenDamage;
        [SerializeField] private List<DamageByType> _damages;

        private Dictionary<IDamageble, Coroutine> _damagebleCoroutineComparer = new Dictionary<IDamageble, Coroutine>();

        public override void AttackByModifier(Collider objectForAttack)
        {
            if (isActiveAndEnabled == true)
            {
                if (SupportingScripts.FindHelper.SearchInIerarhiy(objectForAttack.gameObject, out IDamageble damageble))
                {
                    if (_damagebleCoroutineComparer.ContainsKey(damageble) == false)
                    {
                        var damagingCoroutine = StartCoroutine(Damaging(damageble));
                        _damagebleCoroutineComparer.Add(damageble, damagingCoroutine);
                    }
                }
            }
        }

        private IEnumerator Damaging(IDamageble damageble)
        {
            foreach (var damage in _damages)
                damageble.ApplyDamage(damage);
            yield return new WaitForSeconds(_timeBetweenDamage);
            _damagebleCoroutineComparer.Remove(damageble);
        }

        public override void OnDisable()
        {
            Debug.Log($"Модуль {typeof(DamageModForAttack).Name} на {gameObject.name} отключен!");
            StopAllCoroutines();
            _damagebleCoroutineComparer.Clear();
        }

        public override void OnEnable()
        {
            Debug.Log($"Модуль {typeof(DamageModForAttack).Name} на {gameObject.name} активирован!");
        }
    }
}