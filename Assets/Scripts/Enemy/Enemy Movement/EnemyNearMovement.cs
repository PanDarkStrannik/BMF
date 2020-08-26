using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyNearMovement : AEnemyMovement
{
    
    [SerializeField] private float minDistance = 7f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float timeToRandomMove=1f;
    [SerializeField] private float minRunDistance = 1f;
    [SerializeField] private float maxRunDistance=2f;
    [SerializeField] private bool warp = false;

    public bool near = false;

    private bool isTimeStart=false;
    

    private void Start()
    {
        navAgent.speed = speed;
    }

    public override void Move(Vector3 playerPosition)
    {
        if(!isTimeStart)
        {
            StartCoroutine(ChangePosition(playerPosition));
        }
    }

    private IEnumerator ChangePosition(Vector3 playerPosition)
    {
        isTimeStart = true;
        navAgent.ResetPath();
        int i = 0;
        //near = false;

        while (i < 50)
        {
            Vector3 toPlayer;
            if (near)
            {
                toPlayer = (playerPosition - transform.position).normalized;
            }
            else
            {
                toPlayer = (transform.position-playerPosition).normalized;
            }
            Vector3 randomDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            Vector3 newPosition = transform.position + (toPlayer + randomDirection) * Random.Range(minRunDistance, maxRunDistance);

           
            var checkNewPos = (playerPosition - newPosition).sqrMagnitude;
            if (checkNewPos <= maxDistance*maxDistance && checkNewPos >= minDistance*minDistance)
            {
                if (warp)
                {
                    navAgent.Warp(newPosition);
                }
                else
                {
                    navAgent.destination = newPosition;
                }
                break;
            }
            if(checkNewPos >= maxDistance*maxDistance)
            {
                near = true;
                break;
            }
            if (checkNewPos<=minDistance*minDistance)
            {
                near = false;
                break;
            }
            i++;
        }
        yield return new WaitForSeconds(timeToRandomMove);
        isTimeStart = false;
    }

}
