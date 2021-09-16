using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.DevelopingSupporting;
using StanMechannic;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class StanArea : MonoBehaviour
{
    [SerializeField] private StanData _stanData;
    [SerializeField] private Color _gizmosColor = Color.blue;
    [SerializeField] private LayerMask _stanLayer;

    private Collider _collider;

    private void Awake()
    {
        var rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;

        _collider.isTrigger = true;
    }

    private void OnValidate()
    {
        var rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;

        _collider.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != null)
        {
            if (SupportingScripts.MathHelper.CheckLayer(_stanLayer, other.gameObject))
            {
                Debug.Log("Слой подходит!");
                if (SupportingScripts.FindHelper.SearchInIerarhiy(other.gameObject, out IStanableObject searchingObject))
                {
                    Debug.Log("Нашли объект для стана!");
                    //if(searchingObject.CanStan == false)
                    //{
                    //    Debug.Log("Можем застанить!");
                        searchingObject.StanObject(_stanData);

                    //}
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawCube(_collider.bounds.center, _collider.bounds.size);
    }
}
