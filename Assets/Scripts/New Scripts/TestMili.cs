using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMili : AWeapon
{

    [SerializeReference] private Rigidbody body;
    [SerializeField] private float forceValue=3f;

    public override void Attack()
    {
        Debug.Log("Должна была произойти атака");
        body.AddForce(forceValue * body.transform.forward, ForceMode.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
