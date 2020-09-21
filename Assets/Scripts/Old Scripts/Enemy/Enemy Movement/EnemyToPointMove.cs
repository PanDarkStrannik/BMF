using UnityEngine;
using UnityEngine.AI;


public class EnemyToPointMove : OldAEnemyMovement
{

    [SerializeField] private bool warp=false;
   
    
    void Start()
    {
        //navAgent.speed = speed;

    }

    public override void Move(Vector3 direction)
    {
       // navAgent.ResetPath();

        //if (warp)
        //{
        //    navAgent.Warp(direction);
        //}
        //else
        //{
        //    navAgent.destination = direction;
        //}
        MoveToPointEvent(direction, speed, warp);
    }

}
