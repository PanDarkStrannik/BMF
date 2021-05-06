using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : APlayerMovement
{

    [SerializeField] private List<PlayerMoveType> moveTypes;
    [SerializeField] private List<float> speeds;
    [SerializeField] private float friction=3f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float vercticalAcceleration = 5f;
                     public float VerticalAcceleration { get => vercticalAcceleration; }
    [SerializeField] private float jumpForce = 1f;
                     public float JumpForce { get => jumpForce;}
    [SerializeField] private float g = 9.8f;
    [SerializeField] private float speedInAir=4f;
    [SerializeField] private float correctToAnim = 100f;
    [SerializeField] private CustomEventValue<float> movementSpeedEvent;

    private Dictionary<PlayerMoveType, float> moveTypeSpeeds;

    private Transform playerTransform;
    private float stepTime;

    //public float test;
    //private bool grounded = false;
    //public delegate void FallingEventHelper(float heigth);
    //public event FallingEventHelper FallingEvent;
    //private bool faling = false;
    //private float groundedPos=0f;
    //[SerializeField] private Rigidbody body;
    //[SerializeField] private SphereCollider groundCheckSphere;
    //[SerializeField] private LayerMask groundCheckMask;

    private PlayerMovement()
    {
        PlayerInformation.GetInstance().PlayerMovement = this;
    }

    private void Start()
    {
        playerTransform = transform.parent;
        moveTypeSpeeds = new Dictionary<PlayerMoveType, float>();

        for (int i = 0; i < moveTypes.Count; i++)
        {
            moveTypeSpeeds.Add(moveTypes[i], speeds[i]);
        }
    }

    private void Update()
    {
       Falling();
       Friction();


        //grounded = Physics.CheckSphere(groundCheckSphere.transform.position,
        //  groundCheckSphere.radius, groundCheckMask, QueryTriggerInteraction.Ignore);

        //if (!grounded && !faling)
        //{
        //    faling = true;
        //    groundedPos = body.transform.position.y;
        //}

        //if(grounded && faling)
        //{
        //    faling = false;
        //    var fallPos = body.transform.position.y;
        //    var heigth = groundedPos - fallPos;
        //    if (Mathf.Abs(heigth) != Mathf.Abs(fallPos))
        //    {
        //        test = heigth;
        //        FallingEvent?.Invoke(heigth);
        //    }
        //} 

        
    }


    private void Friction()
    {
        if (grounded && body.velocity.magnitude > 0)
        {
            var newDirection = -body.velocity * friction;
            newDirection.y = 0;
            body.AddForce(newDirection * Time.deltaTime);
        }
    }



    public override void Move(Vector3 direction)
    {
        body.isKinematic = false;
        bool isCallAlready = false;
        direction = direction.normalized;
        ApplyingStepSound(direction);
        foreach (var type in moveTypeSpeeds)
        {
            if (moveType == type.Key && !isCallAlready)
            {
                isCallAlready = true;
                direction = new Vector3(direction.x * type.Value, direction.y * jumpForce, direction.z * type.Value);
                if (grounded)
                {
                    var toAnim = direction.magnitude/correctToAnim;
                    //movementSpeedEvent.StartEvent(toAnim);
                    body.AddForce(direction * Time.deltaTime * acceleration);
                }
                else
                {
                    var newDirection = direction / speedInAir;
                    newDirection.y = -g / speedInAir;
                    body.AddForce(newDirection * Time.deltaTime * acceleration);
                }
                
            }
        }
    }


    public void VerticalMove(Vector3 dir)
    {
        faling = false;
        body.isKinematic = true;
        playerTransform.Translate(dir * vercticalAcceleration * Time.deltaTime);
    }

    private void ApplyingStepSound(Vector3 dir)
    {
        var audioProvider = PlayerInformation.GetInstance().PlayerController.AudioProvider;
        if(audioProvider != null)
        {
           if(dir != Vector3.zero && grounded)
           {
              switch(moveType)
              {
                  case PlayerMoveType.Slow:
                      stepTime = 0.5f;
                      break;
                  case PlayerMoveType.Fast:
                      stepTime = 0.3f;
                      break;
              }
                audioProvider.PlayOneShotWithTime("Steps", stepTime);
           }
        }
    }

    

    public override IEnumerator ImpulseMove(Vector3 direction, ForceMode forceMode, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        body.velocity = Vector3.zero;
        body.AddForce(direction, forceMode);
    }


}
