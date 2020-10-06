using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    //[SerializeReference] private Animator animator;
    [SerializeReference] private NavMeshAgent navAgent;
    [SerializeField] private List<ObjectAimMod> objectAim;


    //void Start()
    //{
    //    var temp = new List<AEnemyMovement>(animator.GetBehaviours<AEnemyMovement>());
    //    foreach (var e in temp)
    //    {
    //        e.MoveToPoint += Moving;
    //        e.LookToObject += Looking;
    //    }

    //}

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
    }

    private void Looking(Transform target)
    {
        foreach(var e in objectAim)
        {
            NewAim.Aim(target, e);
        }
    }

    //private void Aim(Transform target)
    //{

    //    switch (weapon.ObjectAim.Mod)
    //    {
    //        case ObjectAimMod.AimMod.Full:
    //            TargetRotationFixator.Looking(weapon.ObjectAim.LookingObject, target.position + weapon.ObjectAim.CorrectTargetPosition, TargetRotationFixator.LockRotationAngle.None);
    //            break;
    //        case ObjectAimMod.AimMod.LockYaw:
    //            TargetRotationFixator.Looking(weapon.ObjectAim.LookingObject, target.position + weapon.ObjectAim.CorrectTargetPosition, TargetRotationFixator.LockRotationAngle.Yaw);
    //            break;
    //        case ObjectAimMod.AimMod.LockPitch:
    //            TargetRotationFixator.Looking(weapon.ObjectAim.LookingObject, target.position + weapon.ObjectAim.CorrectTargetPosition, TargetRotationFixator.LockRotationAngle.Pitch);
    //            break;
    //    }

    //}

}
