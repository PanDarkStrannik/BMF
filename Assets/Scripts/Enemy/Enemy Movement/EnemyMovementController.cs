using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{

    [HideInInspector] public bool UponDistance = true;
    [HideInInspector] public Transform target = null;
    [SerializeReference] private List<AEnemyMovement> movements;
    [SerializeReference] private List<Transform> lookingObject;
    [SerializeReference] private NavMeshAgent meshAgent;
    [SerializeReference] private MainEvents mainEvents;
    [SerializeField] private float correctSpeedToAnim=10;

    public bool MoveUponDistance(Transform target, float detectionDistance, AEnemyMovement.EnemyMoveType currentType)
    {
        Vector3 toTarget = target.position - transform.position;
        if (toTarget.magnitude <= detectionDistance)
        {
            //var temp = target.position;
            foreach (var lookObj in lookingObject)
            {
                //temp.y = lookObj.position.y;
                //lookObj.LookAt(temp);
                TargetRotationFixator.Looking(lookObj, target, TargetRotationFixator.LockRotationAngle.Pitch);

            }
            mainEvents.OnAnimEvent(AnimationController.AnimationType.Movement, meshAgent.velocity.magnitude / correctSpeedToAnim);
            Move(currentType, target);
            return true;
        }
        else
        {
            mainEvents.OnAnimEvent(AnimationController.AnimationType.Movement, 0);
        }
        return false;
    }



    public void Move(AEnemyMovement.EnemyMoveType currentType, Transform target)
    {
        foreach (var movement in movements)
        {
            if (movement.moveType == currentType)
            {
                 
                //foreach(var lookObj in lookingObject)
                //{
                //    var newLookAt = new Vector3(target.position.x, target.position.y, target.position.z);
                //    lookObj.rotation.SetLookRotation(newLookAt);
                //}
                movement.Move(target.position);
            }
        }
    }

    public void Move(AEnemyMovement.EnemyMoveType currentType)
    {
        Move(currentType, target);
    }


}
