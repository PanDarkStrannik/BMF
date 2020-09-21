using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyAI : StateMachineBehaviour
{
    [SerializeField] protected LayerMask layerMask;

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

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        EnemyAIStateEvent?.Invoke(AIState.Enter);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        CheckInterestingObjects();
        EnemyAIStateEvent?.Invoke(AIState.Update);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        EnemyAIStateEvent?.Invoke(AIState.Exit);
    }


    protected virtual void CheckInterestingObjects()
    {
        foreach(var obj in interestingObjects)
        {
            if((layerMask.value &  1 << obj.layer) != 0)
            {
                DoSomethingWithObject(obj);
            }
        }
    }

    protected virtual void DoSomethingWithObject(GameObject interestingObject)
    {
        Debug.Log("ИИ что-то сделал");
    }

    public enum AIState
    {
        Enter, Update, Exit
    }
}
