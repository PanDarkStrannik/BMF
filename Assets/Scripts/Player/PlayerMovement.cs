using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : APlayerMovement
{

    [SerializeField] private List<PlayerMoveType> moveTypes;
    [SerializeField] private List<float> speeds;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private Rigidbody body;
    [SerializeField] private SphereCollider groundCheckSphere;
    [SerializeField] private LayerMask groundCheckMask;
    [SerializeField] private float g = 9.8f;
    [SerializeField] private float speedInAir=4f;
    [SerializeField] private MainEvents movementEvents;
    [SerializeField] private float correctToAnim = 100f;
    private Dictionary<PlayerMoveType, float> moveTypeSpeeds;
    private bool grounded = false;

    private void Start()
    {
        moveTypeSpeeds = new Dictionary<PlayerMoveType, float>();

        for (int i = 0; i < moveTypes.Count; i++)
        {
            moveTypeSpeeds.Add(moveTypes[i], speeds[i]);
        }
    }


    private void FixedUpdate()
    {

        grounded = Physics.CheckSphere(groundCheckSphere.transform.position,
            groundCheckSphere.radius, groundCheckMask, QueryTriggerInteraction.Ignore);
    }


    public override void Move(Vector3 direction)
    {
        bool isCallAlready = false;
        direction = direction.normalized;
        foreach (var type in moveTypeSpeeds)
        {
            if (moveType == type.Key && !isCallAlready)
            {
                isCallAlready = true;
                direction = new Vector3(direction.x * type.Value, direction.y * jumpForce, direction.z * type.Value);
                if (grounded)
                {
                    var toAnim = direction.magnitude/correctToAnim;
                    movementEvents.OnAnimEvent(AnimationController.AnimationType.Movement, toAnim);
                    body.AddForce(direction * Time.fixedDeltaTime * 100);
                }
                else
                {
                    var newDirection = direction / speedInAir;
                    newDirection.y = -g;
                    body.AddForce(newDirection * Time.fixedDeltaTime * 100);
                }

                
            }
        }
    }

}
