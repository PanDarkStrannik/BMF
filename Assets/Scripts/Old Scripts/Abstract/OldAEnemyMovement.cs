using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class OldAEnemyMovement : MonoBehaviour, IMovement
{

    [SerializeField] protected float speed = 10f;
   // [SerializeField] protected NavMeshAgent navAgent;

    public delegate void MoveToPointHelper(Vector3 point, float speed, bool warp);
    public event MoveToPointHelper MoveToPoint;

    public EnemyMoveType moveType;

    public abstract void Move(Vector3 direction);
        
    protected void MoveToPointEvent(Vector3 point, float speed, bool warp)
    {
        MoveToPoint?.Invoke(point, speed,warp);
    }

    public enum EnemyMoveType
    {
        Chase, Retreat, StayOnDistance, WarpChase, WarpRetreat, WarpStayOnDistance
    }    
}


