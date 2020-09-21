using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyMovement : AEnemyAI, IMovement
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected bool warp = false;
    [SerializeField] private float randomizeInterestingTimer = 3f;

    private int random=0;

    private float timer = 0f;

    public delegate void MoveToPointHelper(Vector3 point, float speed, bool warp);
    public event MoveToPointHelper MoveToPoint;

    public EnemyMoveType moveType;

    public abstract void Move(Vector3 direction);

    protected void MoveToPointEvent(Vector3 point)
    {
        MoveToPoint?.Invoke(point, speed, warp);
    }

    protected override void CheckInterestingObjects()
    {
        List<GameObject> objectsToMove = new List<GameObject>();
        foreach (var obj in interestingObjects)
        {
            if ((layerMask.value & 1 << obj.layer) != 0)
            {
                objectsToMove.Add(obj);
            }
        }

        if (objectsToMove.Count > 0)
        {
            if (timer == 0)
            {
                random = Random.Range(0, objectsToMove.Count);
            }
            timer += Time.deltaTime;
            if (timer >= randomizeInterestingTimer)
            {
                timer = 0;
            }
            DoSomethingWithObject(objectsToMove[random]);
        }
    }

    protected override void DoSomethingWithObject(GameObject interestingObject)
    {
        Move(interestingObject.transform.position);
    }

    public enum EnemyMoveType
    {
        Chase, Retreat, StayOnDistance, WarpChase, WarpRetreat, WarpStayOnDistance
    }
}
