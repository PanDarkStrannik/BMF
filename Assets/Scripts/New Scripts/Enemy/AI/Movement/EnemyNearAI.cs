using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNearAI : AEnemyMovement
{

    [SerializeField] private float minDistance = 7f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float minRunDistance = 1f;
    [SerializeField] private float maxRunDistance = 2f;
    [SerializeField] private float updateRandomMoveTime = 3f;

    public bool near = false;

    private float timer = 0f;

    public override void Move(Vector3 playerPosition)
    {
        if (timer == 0)
        {
            ChangePosition(playerPosition);
        }
        timer += Time.deltaTime;
        if (timer >= updateRandomMoveTime)
        {
            timer = 0;
        }
    }

    private void ChangePosition(Vector3 playerPosition)
    {
        int i = 0;
        while (i < 200)
        {

            Vector3 toPlayer;
            if (near)
            {
                toPlayer = (playerPosition - aiAgent.transform.position).normalized;
            }
            else
            {
                toPlayer = (aiAgent.transform.position - playerPosition).normalized;
            }
            Vector3 randomDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            Vector3 newPosition = aiAgent.transform.position + (toPlayer + randomDirection) * Random.Range(minRunDistance, maxRunDistance);


            var checkNewPos = (playerPosition - newPosition).sqrMagnitude;
    
            if (checkNewPos >= maxDistance * maxDistance)
            {
                near = true;
            }
            if (checkNewPos <= minDistance * minDistance)
            {
                near = false;
            }
            if (checkNewPos <= maxDistance * maxDistance && checkNewPos >= minDistance * minDistance)
            {
                MoveToPointEvent(newPosition);
                break;
            }
        }
    }

}
