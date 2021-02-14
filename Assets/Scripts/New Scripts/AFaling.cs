using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFaling : MonoBehaviour
{
    [SerializeReference] public Rigidbody body;
    [SerializeField] protected SphereCollider groundCheckSphere;
    [SerializeField] protected LayerMask groundCheckMask;

    protected bool grounded = false;

    public delegate void FallEventHelper(float heigth);
    public event FallEventHelper FallEvent;

    public delegate void FailingEventHelper(bool failing);
    public event FailingEventHelper FalingEvent;

    protected bool faling = false;

    private float groundedPos = 0f;

    public float test=0f;

    protected void Falling()
    {
        grounded = Physics.CheckSphere(groundCheckSphere.transform.position,
            groundCheckSphere.radius, groundCheckMask, QueryTriggerInteraction.Ignore);

        if (!grounded && !faling)
        {
            faling = true;
            FalingEvent?.Invoke(faling);
            groundedPos = body.transform.position.y;
        }

        if (grounded && faling)
        {
            faling = false;
            FalingEvent?.Invoke(faling);
            var fallPos = body.transform.position.y;
            var heigth = groundedPos - fallPos;
            
            //if (Mathf.Abs(heigth) != Mathf.Abs(fallPos))
            //{
                test = heigth;
                FallEvent?.Invoke(heigth);
           // }
        }
    }
}
