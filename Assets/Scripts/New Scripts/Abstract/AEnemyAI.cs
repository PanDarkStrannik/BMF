using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyAI : StateMachineBehaviour
{
    [SerializeField] protected LayerMask layerMask;

    [SerializeField] protected bool isRigidbodyKinematick = true;
    [SerializeField] protected bool isNavMeshAgentActive = true;

    public delegate void BoolEventHelper(bool value);
    public event BoolEventHelper RigidbodyActiveEvent;
    public event BoolEventHelper NavMeshAgentActiveEvent;

    protected GameObject currentInteresting = null;


    protected GameObject aiAgent;
    public GameObject AIAgent
    {
        set
        {
            aiAgent = value;
        }
    }

    [SerializeField] protected List<GameObject> interestingObjects;
    public List<GameObject> InterestingObjects
    {
        set
        {
            interestingObjects = value;
        }
    }

    public delegate void EnemyAIStateEventHelper(AIState state);
    public event EnemyAIStateEventHelper EnemyAIStateEvent;

    #region Стейты
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        InStateStart();
        EnemyAIStateEvent?.Invoke(AIState.Enter);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        InStateUpdate();
        RigidbodyActiveEvent?.Invoke(isRigidbodyKinematick);
        NavMeshAgentActiveEvent?.Invoke(isNavMeshAgentActive);
        EnemyAIStateEvent?.Invoke(AIState.Update);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        InStateExit();
        EnemyAIStateEvent?.Invoke(AIState.Exit);
    }
    #endregion


    #region Действия в стейтах

    protected virtual void InStateStart()
    {

    }

    protected virtual void InStateUpdate()
    {

    }


    protected virtual void InStateExit()
    {

    }

    #endregion


    #region Вспомогательное

    protected virtual List<GameObject> CheckInterestingObjects()
    {
        List<GameObject> tempInterestings = new List<GameObject>();
        foreach (var obj in interestingObjects)
        {
            if ((layerMask.value & 1 << obj.layer) != 0)
            {
                tempInterestings.Add(obj);
            }
        }

        if (tempInterestings.Count > 0)
        {
            return tempInterestings;
        }
        throw new System.Exception("Ничего интересного не видно!");
    }


    protected GameObject GetRandomInList(List<GameObject> interestings)
    {        
        int random = Random.Range(0, interestings.Count);
        return interestings[random];
    }

    protected virtual void InteractWithObject(GameObject interestingObject)
    {
        Debug.Log("ИИ что-то сделал");
    }

    public enum AIState
    {
        Enter, Update, Exit
    }

    #endregion
}
