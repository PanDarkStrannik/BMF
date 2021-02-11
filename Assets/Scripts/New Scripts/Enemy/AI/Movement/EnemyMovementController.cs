using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMovementController : AFaling
{
    //[SerializeReference] private Animator animator;
    [SerializeReference] private NavMeshAgent navAgent;
    [SerializeField] private List<ObjectAimMod> objectAim;
    [SerializeField] private CustomEventValue<bool> moving;

    //void Start()
    //{
    //    var temp = new List<AEnemyMovement>(animator.GetBehaviours<AEnemyMovement>());
    //    foreach (var e in temp)
    //    {
    //        e.MoveToPoint += Moving;
    //        e.LookToObject += Looking;
    //    }

    //}

    private void Start()
    {
       // navAgent.updateRotation = false;
    }

    private void Update()
    {
        Falling();
        if(!grounded)
        {
            body.AddForce(new Vector3(0, -9.8f, 0) * Time.deltaTime);
        }
    }


    public void Initialize(List<AEnemyMovement> enemyAIs)
    {
        foreach (var e in enemyAIs)
        {
            e.MoveToPoint += Moving;
            e.LookToObject += Looking;
        }
    }


    public void Deinitialize(List<AEnemyMovement> enemyAIs)
    {
        foreach (var e in enemyAIs)
        {
            e.MoveToPoint -= Moving;
            e.LookToObject -= Looking;
        }
    }


    private void Moving(Vector3 point, float speed, bool warp)
    {
        if (navAgent.isActiveAndEnabled)
        {
            moving.StartEvent(true);
            navAgent.speed = speed;
            if (warp)
            {
                navAgent.Warp(point);
            }
            else
            {
                navAgent.SetDestination(point);
            }
        }
        else
        {
            moving.StartEvent(false);
        }
    }

    private void Looking(Transform target)
    {
        foreach(var e in objectAim)
        {
            NewAim.Aim(target, e);
        }
    }

    //private void FixedUpdate()
    //{


    //    grounded = Physics.CheckSphere(groundCheckSphere.transform.position,
    //          groundCheckSphere.radius, groundCheckMask, QueryTriggerInteraction.Ignore);

    //    if (!grounded && !faling)
    //    {
    //        faling = true;
    //        groundedPos = body.transform.position.y;
    //    }

    //    if (grounded && faling)
    //    {
    //        faling = false;
    //        var fallPos = body.transform.position.y;
    //        var heigth = groundedPos - fallPos;
    //        if (Mathf.Abs(heigth) != Mathf.Abs(fallPos))
    //        {
    //            test = heigth;
    //            FallingEvent?.Invoke(heigth);
    //        }
    //    }

    //}
}
