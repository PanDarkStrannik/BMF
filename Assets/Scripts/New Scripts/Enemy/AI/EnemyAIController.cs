using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyDetection detection;
    [SerializeField] private GameObject enemyObject;
    [SerializeReference] private MainEvents mainEvents;

    [SerializeField] private EnemyEventValueFSM<bool> PlayerDetectedEvent;
    [SerializeField] private EnemyEventValueFSM<float> PlayerDistanceEvent;

    private List<AEnemyAI> behaviours;

    public List<AEnemyAI> Behaviours
    {
        get
        {
            return behaviours;
        }
    }

    private void Start()
    {
        behaviours = new List<AEnemyAI>(animator.GetBehaviours<AEnemyAI>());

        foreach(var beh in behaviours)
        {
            beh.AIAgent = enemyObject;
        }

        detection.DetectedObjectsEvent += ChangeInterestingAIObjects;
        
    }

    private void ChangeInterestingAIObjects(List<GameObject> detectedObjects)
    {
        foreach(var obj in detectedObjects)
        {
            if(obj.CompareTag("Player"))
            {
                PlayerDetectedEvent.StartEvent(true);
               // mainEvents.DetectedObjectEvent(obj.transform);
                var distance = Vector3.Distance(obj.transform.position, enemyObject.transform.position);
                PlayerDistanceEvent.StartEvent(distance);
                break;
            }
            else
            {
                PlayerDetectedEvent.StartEvent(false);
            }
        }

        foreach(var beh in behaviours)
        {
            beh.InterestingObjects = detectedObjects;
        }
    }
}

[System.Serializable]
public class EnemyEventFSM
{
    [SerializeField] private string triggerName;
    [SerializeField] private UnityEvent<string> unityEvent;

    public void StartEvent()
    {
        unityEvent?.Invoke(triggerName);
    }
}

[System.Serializable]
public class EnemyEventValueFSM<T>
{
    [SerializeField] private string triggerName;
    [SerializeField] private UnityEvent<string, T> unityEvent;

    public void StartEvent(T value)
    {
        unityEvent?.Invoke(triggerName, value);
    }
}