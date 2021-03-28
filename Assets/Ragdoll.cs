using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] rigidBodies;
    private Collider[] cols;

    [SerializeField] private Vector3 forceTorque;
    [SerializeField] private Vector3 force;
    [SerializeField] private ForceMode forceMode;
    [SerializeReference] private Rigidbody centralBody;

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

        foreach (var rb in rigidBodies)
        {
            rb.isKinematic = isKinematic;
            
        }
        foreach (var col in cols)
        {
            col.enabled = !isKinematic;
        }
        if(!isKinematic)
        {
         centralBody.AddForceAtPosition(-forceDir * force.z + Vector3.up * force.y, centralBody.transform.position, forceMode);
        }
    }
}
