using StateMechanic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMovementController : AController
{
    [SerializeReference] private NavMeshAgent navAgent;
    [SerializeField] private List<ObjectAimMod> objectAim;
    [SerializeField] private CustomEventValue<bool> moving;

    private bool alreadySubscribe = false;
    private List<AEnemyMovement> enemyMovements = null;

    //private void Update()
    //{
    //    Falling();
    //    if(grounded==false)
    //    {
    //        navAgent.enabled = false;
    //        body.AddForce(new Vector3(0, -9.8f, 0) * Time.deltaTime);
    //    }
    //    else
    //    {
    //        //navAgent.enabled = true;
    //    }
    //}


    private void OnEnable()
    {
        navAgent.enabled = true;
        SubscribeOnEvents();
    }

    private void OnDisable()
    {
        UnSubscribeOnEvents();
        navAgent.enabled = false;
        moving.StartEvent(false);
    }


    private void SubscribeOnEvents()
    {
        if (enemyMovements != null)
        {
            if (alreadySubscribe == false)
            {
                foreach (var e in enemyMovements)
                {
                    e.MoveToPoint += Moving;
                    e.LookToObject += Looking;
                }
            }
            alreadySubscribe = true;
        }
    }

    private void UnSubscribeOnEvents()
    {
        if (enemyMovements != null)
        {
            if (alreadySubscribe == true)
            {
                foreach (var e in enemyMovements)
                {
                    e.MoveToPoint -= Moving;
                    e.LookToObject -= Looking;
                }
            }
            alreadySubscribe = false;
        }
    }


    public void Initialize(List<AEnemyMovement> enemyAIs)
    {
        enemyMovements = enemyAIs;
        SubscribeOnEvents();
    }




    public void Deinitialize(List<AEnemyMovement> enemyAIs)
    {
        UnSubscribeOnEvents();
        enemyMovements = null;
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
            NewAim.NormalAim(target, e);
        }
    }
}
