using UnityEngine;

namespace AttackModifier
{
    public abstract class AAttackModifier : MonoBehaviour
    {
        public abstract void OnEnable();
        public abstract void AttackByModifier(Collider objectForAttack);
        public abstract void OnDisable();
    }
}