using UnityEngine;
using CharacterStateMechanic;

public class AFaling : AController
{
    [SerializeReference] public Rigidbody body;
    [SerializeField] protected SphereCollider groundCheckSphere;
    [SerializeField] protected LayerMask groundCheckMask;

    [SerializeField] private bool disableFalling = false;

    protected bool grounded = false;
    public bool Grounded { get => grounded; }

    public delegate void FallEventHelper(float heigth);
    public event FallEventHelper FallEvent;

    public delegate void FailingEventHelper(bool failing);
    public event FailingEventHelper FalingEvent;


    protected bool faling = false;

    private float groundedPos = 0f;

    public float test=0f;

    protected void Falling()
    {
        if(disableFalling)
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
        else
        {
            grounded = true;
        }
    }
}
