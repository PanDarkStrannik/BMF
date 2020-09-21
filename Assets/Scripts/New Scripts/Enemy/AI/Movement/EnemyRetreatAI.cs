using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRetreatAI : AEnemyMovement
{
    [SerializeField] private float distance = 3f;
    

    public override void Move(Vector3 playerPosition)
    {
        Vector3 retreatVector = (aiAgent.transform.position - playerPosition).normalized * distance;
        Vector3 newPosition = aiAgent.transform.position + retreatVector;
        MoveToPointEvent(newPosition);
    }

}
