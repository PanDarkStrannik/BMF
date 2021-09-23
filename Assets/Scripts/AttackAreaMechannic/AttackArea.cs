using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AttackModifier;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class AttackArea : MonoBehaviour
{
    [SerializeField] private Color _areaColor;
    [SerializeField] private LayerMask _layerToAttack;
    [SerializeField] private List<AAttackModifier> _attackModifiers;
    [SerializeField] private List<EventOnAttackLayer> _attackLayersEvents;

    private Collider _areaColider = null;

    private void Awake()
    {
        _areaColider = GetComponent<Collider>();
        var rigidbody = GetComponent<Rigidbody>();

        _areaColider.isTrigger = true;
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
    }

    private void OnEnable()
    {
        foreach (var attackModifier in _attackModifiers)
            attackModifier.enabled = true;
    }

    private void OnDisable()
    {
        foreach (var attackModifier in _attackModifiers)
            attackModifier.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {
            if (CheckLayer(other.gameObject) == true)
            {
                foreach (var eventOnAttackLayer in _attackLayersEvents)
                    eventOnAttackLayer.InvokeByLayer(other.gameObject.layer);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != null)
        {
            if (CheckLayer(other.gameObject) == true)
            {
                foreach (var attackModifier in _attackModifiers)
                    attackModifier.AttackByModifier(other);
            }
        }
    }


    private bool CheckLayer(GameObject gameObject)
    {
        if ((_layerToAttack.value & 1 << gameObject.layer) != 0)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (_areaColider == null)
            _areaColider = GetComponent<Collider>();
        Gizmos.color = _areaColor;
        Gizmos.DrawCube(_areaColider.bounds.center, _areaColider.bounds.size);
    }
}

[System.Serializable]
public class EventOnAttackLayer
{
    public LayerMask whoIsTarget;
    public UnityEvent OnDamaged;

    public void InvokeByLayer(LayerMask layer)
    {
        if((whoIsTarget.value & 1 << layer)!= 0)
            OnDamaged?.Invoke();
    }
  
}
