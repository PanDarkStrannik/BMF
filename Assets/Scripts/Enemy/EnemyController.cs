﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovementController))]
public class EnemyController : MonoBehaviour
{

    [SerializeField] private List<EnemyEventMoveType> enemyMoveEvents;
    [SerializeField] private List<PlayerEventMoveType> playerMoveEvents;

    [SerializeField] private float timeAfterDetect=1f;

    [SerializeField] private float attackDelay = 1f;

    [SerializeReference] private EnemyMovementController movement;
    [SerializeReference] private EnemyDetection detection;
    [SerializeReference] private EnemyWeaponLogic weapon;
    [SerializeReference] private MainEvents mainEvents;

    private bool isAttackRecently = false;

    public void Start()
    {

  

        GameEvents.PlayerAction += OnPlayerEvent;
        GameEvents.EnemyAction += OnEnemyEvent;
        detection.DetectingEvent += delegate
        {
            StartCoroutine(OnDetecting());
        };

    }


    private void OnDisable()
    {

        detection.DetectingEvent -= delegate
        {
            StartCoroutine(OnDetecting());
        };
    }


    private IEnumerator OnDetecting()
    {
        mainEvents.OnAnimEvent(AnimationController.AnimationType.Start);
        yield return new WaitForSeconds(timeAfterDetect);
        while (detection.AlreadyDetect)
        {
            var detectionObjects = detection.DetectionsObjects;
            if (detectionObjects != null)
            {
                foreach (var obj in detectionObjects)
                {
                    if (obj.CompareTag("Player"))
                    {
                        mainEvents.DetectedObjectEvent(obj.transform);

                        MovementLogic(obj.transform);

                        if (!isAttackRecently)
                        {
                            StartCoroutine(AttackLogic());
                        }
                        break;
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void MovementLogic(Transform objTransform)
    {
        if (movement.MoveUponDistance(objTransform, detection.DetectedColider.Radius, detection.DetectedColider.MoveType))
        {
           
        }
    }



    private IEnumerator AttackLogic()
    {
        if (weapon.Attack(detection.DetectedColider.WeaponType))
        {
            isAttackRecently = true;
            yield return new WaitForSeconds(attackDelay);
            isAttackRecently = false;
        }
    }


    #region Действия при эвентах


    private void OnPlayerEvent(GameEvents.PlayerEvents playerEvent)
    {
        if (playerMoveEvents.Count > 0)
        {
            foreach (var element in playerMoveEvents)
            {
                if (playerEvent == element.EventType)
                {                    
                    movement.Move(element.MoveType);
                    break;
                }
            }
        }
    }



    private void OnEnemyEvent(GameEvents.EnemyEvents enemyEvent)
    {
        if (playerMoveEvents.Count > 0)
        {
            foreach (var element in enemyMoveEvents)
            {
                if (enemyEvent == element.EventType)
                {
                    movement.UponDistance = false;
                    movement.Move(element.MoveType);
                    break;
                }
            }
        }
    }

    #endregion





}

#region Вспомогательные классы

[System.Serializable]
public class EnemyEventMoveType
{
    [SerializeField] private AEnemyMovement.EnemyMoveType enemyMoveType;
    [SerializeField] private GameEvents.EnemyEvents enemyEventType;

    public AEnemyMovement.EnemyMoveType MoveType
    {
        get
        {
            return enemyMoveType;
        }
    }

    public GameEvents.EnemyEvents EventType
    {
        get
        {
            return enemyEventType;
        }
    }

}

[System.Serializable]
public class PlayerEventMoveType
{
    [SerializeField] private AEnemyMovement.EnemyMoveType enemyMoveType;
    [SerializeField] private GameEvents.PlayerEvents playerEventType;

    public AEnemyMovement.EnemyMoveType MoveType
    {
        get
        {
            return enemyMoveType;
        }
    }

    public GameEvents.PlayerEvents EventType
    {
        get
        {
            return playerEventType;
        }
    }
}

#endregion