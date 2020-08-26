using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class AEnemyMovement : MonoBehaviour, IMovement
{

    [SerializeField] protected float speed = 10f;
    [SerializeField] protected NavMeshAgent navAgent;

    public EnemyMoveType moveType;

    public abstract void Move(Vector3 direction);
        

    public enum EnemyMoveType
    {
        Chase, Retreat, StayOnDistance, WarpChase, WarpRetreat, WarpStayOnDistance
    }    
}


