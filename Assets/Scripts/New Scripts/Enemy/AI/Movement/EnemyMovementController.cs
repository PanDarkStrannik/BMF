using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navAgent;

    void Start()
    {
        var temp = new List<AEnemyMovement>(animator.GetBehaviours<AEnemyMovement>());
        foreach (var e in temp)
        {
            e.MoveToPoint += Moving;
        }

    }

    private void Moving(Vector3 point, float speed, bool warp)
    {
        navAgent.speed = speed;
        if (warp)
        {
            navAgent.Warp(point);
        }
        else
        {
            navAgent.SetDestination(point);
        }
        Debug.Log("Сробило");
    }

}
