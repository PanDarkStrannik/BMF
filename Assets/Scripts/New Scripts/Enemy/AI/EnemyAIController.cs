using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyDetection detection;
    [SerializeField] private GameObject enemyObject;

    [SerializeField] private CustomEventValue<bool> PlayerDetectedEvent;
    [SerializeField] private CustomEventValue<float> PlayerDistanceEvent;
    [SerializeField] private List<CustomEventValue<AWeapon.WeaponState, bool>> PlayerWeaponControllerEvents;

    [SerializeReference] private AttackController attackController;
    [SerializeReference] private EnemyMovementController movementController;
    [SerializeReference] private Rigidbody rb;
    [SerializeReference] private NavMeshAgent navMeshAgent;
 
    private List<AEnemyAI> behaviours;

    public List<AEnemyAI> Behaviours
    {
        get
        {
            return behaviours;
        }
    }


    private List<AttackAI> attackAIs;
    private List<AEnemyMovement> movementsAIs;

    private bool faling = false;

    private void OnEnable()
    {

        behaviours = new List<AEnemyAI>(animator.GetBehaviours<AEnemyAI>());

        movementController.FalingEvent += delegate (bool value) { faling = value; };

        foreach (var beh in behaviours)
        {
            beh.AIAgent = enemyObject;
            beh.RigidbodyActiveEvent += delegate (bool value)
            {
                if (faling)
                {
                    rb.isKinematic = false;
                }
                else
                {
                    rb.isKinematic = value;
                }
            };
            beh.NavMeshAgentActiveEvent += delegate (bool value)
            {
                if (faling)
                {
                    navMeshAgent.enabled = false;
                }
                else
                {
                    navMeshAgent.enabled = value;
                }
            };
        }

        detection.DetectedObjectsEvent += ChangeInterestingAIObjects;
        // GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayerWeaponControlEvent += PlayerWeaponControllerEventListener;

       // PlayerInformation.GetInstance().PlayerController.PlayerWeaponControlEvent += PlayerWeaponControllerEventListener;

        attackAIs = new List<AttackAI>(animator.GetBehaviours<AttackAI>());
        attackController.Initialize(attackAIs);      

        movementsAIs = new List<AEnemyMovement>(animator.GetBehaviours<AEnemyMovement>());
        movementController.Initialize(movementsAIs);
    }

    private void OnDisable()
    {
        detection.DetectedObjectsEvent -= ChangeInterestingAIObjects;
        // GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayerWeaponControlEvent -= PlayerWeaponControllerEventListener;
        PlayerInformation.GetInstance().PlayerController.PlayerWeaponControlEvent -= PlayerWeaponControllerEventListener;


        attackController.Deinitialize(attackAIs);
        movementController.Deinitialize(movementsAIs);

        foreach (var beh in behaviours)
        {
            beh.RigidbodyActiveEvent -= delegate (bool value) { rb.isKinematic = value; };
            beh.NavMeshAgentActiveEvent -= delegate (bool value) { navMeshAgent.enabled = value; };
        }

    }


    //private void Start()
    //{
    //    behaviours = new List<AEnemyAI>(animator.GetBehaviours<AEnemyAI>());

    //    foreach(var beh in behaviours)
    //    {
    //        beh.AIAgent = enemyObject;
    //        beh.RigidbodyActiveEvent += delegate (bool value) { rb.isKinematic = value; };
    //        beh.NavMeshAgentActiveEvent += delegate (bool value) { navMeshAgent.enabled = value; };
    //    }

    //}

    private void ChangeInterestingAIObjects(List<GameObject> detectedObjects)
    {

        foreach (var obj in detectedObjects)
        {
            if (obj.CompareTag("Player"))
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

        foreach (var beh in behaviours)
        {
            beh.InterestingObjects = detectedObjects;
        }

    }

    private void PlayerWeaponControllerEventListener(AWeapon.WeaponState controlType)
    {
        foreach (var e in PlayerWeaponControllerEvents)
        {
            if (e.GetStringValue == controlType)
            {
                e.StartEvent(true);
            }
            else
            {
                e.StartEvent(false);
            }
        }
    }
}

[System.Serializable]
public class CustomEventNotValue
{
    [SerializeField] private string triggerName;
    [SerializeField] private UnityEvent<string> unityEvent;

    public void StartEvent()
    {
        unityEvent?.Invoke(triggerName);
    }
}

[System.Serializable]
public class CustomEventValue<T>
{
    [SerializeField] private string triggerName;
    [SerializeField] private UnityEvent<string, T> unityEvent;

    public void StartEvent(T value)
    {
        unityEvent?.Invoke(triggerName, value);
    }
}

[System.Serializable]
public class CustomEventValue<T,A>
{
    [SerializeField] private string addStringName;
    [SerializeField] private T stringValue;
    [SerializeField] private UnityEvent<string, A> unityEvent;

    public T GetStringValue
    {
        get
        {
            return stringValue;
        }
    }

    public void StartEvent(A value)
    {
        unityEvent?.Invoke(addStringName + stringValue.ToString(), value);
    }
}