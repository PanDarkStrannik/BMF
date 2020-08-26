using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerOldMovement : APlayerMovement
{
    [SerializeField] private List<PlayerMoveType> moveTypes;
    [SerializeField] private List<float> speeds;
    [SerializeField] private float g=9.8f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float jumpTime = 1f;


    private CharacterController controller;
    private Dictionary<PlayerMoveType, float> moveTypeSpeeds;

    private void Start()
    {
        moveTypeSpeeds = new Dictionary<PlayerMoveType, float>();
        controller = GetComponent<CharacterController>();

        for (int i = 0; i < moveTypes.Count; i++)
        {
            moveTypeSpeeds.Add(moveTypes[i], speeds[i]);
        }
    }



    public override void Move(Vector3 direction)
    {
        foreach(var type in moveTypeSpeeds)
        {
            if (moveType == type.Key)
            {
                direction = new Vector3(direction.x, 0, direction.z);

                //if (Jump)
                //{
                //    var correctDirection = (direction.normalized + transform.up) * Time.deltaTime * (jumpForce - g);
                //    controller.Move(correctDirection); 
                //    Invoke("DisableJump", jumpTime);
                //}

                //if (controller.isGrounded)
                //{
                //    controller.Move(direction.normalized * Time.deltaTime * type.Value);
                //}
                //else if (!Jump)
                //{
                //    var correctDirection = (direction.normalized - transform.up) * Time.deltaTime * g;
                //    controller.Move(correctDirection);
                //}

                //body.AddForce(direction.normalized * type.Value);
            }
        }
    }

    private void DisableJump()
    {
        //Jump = false;
    }

}
