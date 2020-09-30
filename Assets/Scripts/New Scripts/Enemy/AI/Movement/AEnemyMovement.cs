using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyMovement : AEnemyAI, IMovement
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected bool warp = false;
    [SerializeField] private float randomizeInterestingTimer = 3f;


    private float timer = 0f;

    public delegate void MoveToPointHelper(Vector3 point, float speed, bool warp);
    public event MoveToPointHelper MoveToPoint;

    public delegate void LookToObjectHelper(Transform target);
    public event LookToObjectHelper LookToObject;

    public EnemyMoveType moveType;

    public abstract void Move(Vector3 direction);

    protected void MoveToPointEvent(Vector3 point)
    {
        MoveToPoint?.Invoke(point, speed, warp);
    }


    protected override void InStateUpdate()
    {
        if (timer == 0)
        {
            currentInteresting = GetRandomInList(CheckInterestingObjects());
        }
        timer += Time.deltaTime;
        if (timer >= randomizeInterestingTimer)
        {
            timer = 0;
        }
        if (currentInteresting != null)
        {
            InteractWithObject(currentInteresting);
        }
    }

    //protected override void CheckInterestingObjects()
    //{
    //    List<GameObject> objectsToMove = new List<GameObject>();
    //    foreach (var obj in interestingObjects)
    //    {
    //        if ((layerMask.value & 1 << obj.layer) != 0)
    //        {
    //            objectsToMove.Add(obj);
    //        }
    //    }

    //    if (objectsToMove.Count > 0)
    //    {
    //        if (timer == 0)
    //        {
    //            random = Random.Range(0, objectsToMove.Count);
    //        }
    //        timer += Time.deltaTime;
    //        if (timer >= randomizeInterestingTimer)
    //        {
    //            timer = 0;
    //        }
    //        DoSomethingWithObject(objectsToMove[random]);
    //    }
    //}

    protected override void InteractWithObject(GameObject interestingObject)
    {
        LookToObject?.Invoke(interestingObject.transform);
        Move(interestingObject.transform.position);
    }

    public enum EnemyMoveType
    {
        Chase, Retreat, StayOnDistance, WarpChase, WarpRetreat, WarpStayOnDistance
    }
}
