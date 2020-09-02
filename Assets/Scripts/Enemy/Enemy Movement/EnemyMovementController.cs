using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(Animator))]
public class EnemyMovementController : MonoBehaviour
{

    [HideInInspector] public bool UponDistance = true;
    [HideInInspector] public Transform target = null;
    [SerializeReference] private List<AEnemyMovement> movements;
    [SerializeReference] private NavMeshAgent meshAgent;
    [SerializeReference] private MainEvents mainEvents;
    [SerializeField] private float correctSpeedToAnim=10;

    //private Animator anim;

    //private Vector3 startPosition;

    private void Start()
    {
        //anim = GetComponent<Animator>();
        //startPosition = transform.localPosition;
        foreach(var e in movements)
        {
            e.MoveToPoint += Moving;
        }
    }


    //private void Update()
    //{
    //    //    Vector3 worldDeltaPosition = meshAgent.nextPosition - transform.position;

    //    //    if (worldDeltaPosition.magnitude > meshAgent.radius)
    //    //        meshAgent.nextPosition = transform.position + 0.9f * worldDeltaPosition;

    //    Vector3 worldDeltaPosition = meshAgent.gameObject.transform.position - transform.position;

    //    if (worldDeltaPosition.magnitude > meshAgent.radius)
    //        meshAgent.gameObject.transform.position = transform.position; //+ 0.9f * worldDeltaPosition;

    //}

    private void LateUpdate()
    {
        mainEvents.OnAnimEvent(AnimationController.AnimationType.Movement, meshAgent.velocity.magnitude / correctSpeedToAnim);
    }


    private void Moving(Vector3 point, float speed, bool warp)
    {
        meshAgent.ResetPath();
        meshAgent.speed = speed;
        if (warp)
        {
            meshAgent.Warp(point);
        }
        else
        {
            meshAgent.destination = point;
        }
    }


    public bool MoveUponDistance(Transform target, float detectionDistance, AEnemyMovement.EnemyMoveType currentType)
    {
        Vector3 toTarget = target.position - transform.position;
        if (toTarget.magnitude <= detectionDistance)
        {
            Move(currentType, target);
            return true;
        }
        return false;
    }



    public void Move(AEnemyMovement.EnemyMoveType currentType, Transform target)
    {
        foreach (var movement in movements)
        {
            if (movement.moveType == currentType)
            {
                movement.Move(target.position);
            }
        }
    }

    public void Move(AEnemyMovement.EnemyMoveType currentType)
    {
        Move(currentType, target);
    }


    //private void OnAnimatorMove()
    //{
    //    //Vector3 worldDeltaPosition = meshAgent.nextPosition - transform.position;

    //    //if (worldDeltaPosition.magnitude > meshAgent.radius)
    //    //    meshAgent.gameObject.transform.position = transform.position + 0.9f * worldDeltaPosition;

    //    //var position = meshAgent.nextPosition;
    //    //position.y = anim.rootPosition.y;
    //    //transform.position = position;
    //   // transform.position = anim.rootPosition;

    //    //if (anim.hasRootMotion)
    //    //{
    //        transform.position = anim.rootPosition;
    //    //    Debug.Log("У анимации есть рут моушин");
    //    //}
    //    //else
    //    //{
    //    //    transform.localPosition = startPosition;
    //    //}

    //}

}
