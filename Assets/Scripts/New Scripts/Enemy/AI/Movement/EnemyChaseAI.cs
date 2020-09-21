using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseAI : AEnemyMovement
{
    public override void Move(Vector3 direction)
    {
        MoveToPointEvent(direction);
    }
}
