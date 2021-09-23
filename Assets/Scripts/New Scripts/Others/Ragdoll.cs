using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Joint _jointForConnect;
    [SerializeField] private Rigidbody _rigidbodyForConnect;
    [SerializeField] private Vector3 force;
    [SerializeField] private ForceMode forceMode;
    [SerializeField] private Rigidbody centralBody;
    [SerializeField] private Animator _animator;

    private Rigidbody[] rigidBodies;
    private Collider[] cols;

    private void Awake()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        cols = GetComponentsInChildren<Collider>();

        foreach (var rb in rigidBodies)
        {
            rb.isKinematic = true;
        }
        foreach (var col in cols)
        {
            col.enabled = false;
        }
        
    }

    public void RagdollControll(bool isKinematic)
    {
        var playerPos = PlayerInformation.GetInstance().Player.transform;
        Vector3 forceDir = playerPos.position - centralBody.transform.position;
        forceDir.Normalize();

        ChangeRagdoll(isKinematic);

        if (!isKinematic)
        {
            centralBody.AddForceAtPosition(-forceDir * force.z + Vector3.up * force.y, centralBody.transform.position, forceMode);
        }
    }

    public void ChangeRagdoll(bool isKinematic)
    {
        _animator.enabled = isKinematic;
        foreach (var rb in rigidBodies)
        {
            rb.isKinematic = isKinematic;

        }
        foreach (var col in cols)
        {
            col.enabled = !isKinematic;
        }
        if (isKinematic == false)
        {
            //_rigidbodyForConnect.isKinematic = false;
            _jointForConnect.connectedBody = _rigidbodyForConnect;
        }
        else
        {
            //_rigidbodyForConnect.isKinematic = true;
            _jointForConnect.connectedBody = null;
            _rigidbodyForConnect.velocity = Vector3.zero;
            foreach (var rb in rigidBodies)
            {
                rb.velocity = Vector3.zero;

            }
        }
    }
}
