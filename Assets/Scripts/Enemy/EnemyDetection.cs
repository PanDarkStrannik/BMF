using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private List<ColliderChance> colliderChances;
    [Range(0.1f,30f)]
    [SerializeField] private float timeToCheck=1f;
    [Range(0.1f,30f)]
    [SerializeField] private float timeToForget = 1f;
    [SerializeField] private bool detecting = true;
    [SerializeField] private bool forgeting = true;

    private ColliderChance detectedColider;

    private List<GameObject> detections;

    private int detectionsInColliders = 0;

    public delegate void DetectingHelper();
    public event DetectingHelper DetectingEvent;

    private bool isForgoting = false;
    private bool alreadyDetect = false;
    public bool AlreadyDetect
    {
        get
        {
            return alreadyDetect;
        }
    }


    public ColliderChance DetectedColider
    {
        get
        {
            return detectedColider;
        }
    }


    public List<GameObject> DetectionsObjects
    {
        get
        {
            if(detections.Count>0)
            {
                return detections;
            }
            return null;
        }
    }

    


    public bool Detecting
    {
        get
        {
            return detecting;
        }
        set
        {
            detecting = value;
            if(detecting==true)
            {
                StartCoroutine(Detection());
            }
        }
    }


    public bool Forgeting
    {
        get
        {
            return forgeting;
        }
        set
        {
            forgeting = value;
        }
    }




    private void Start()
    {
        detections = new List<GameObject>();
        StartCoroutine(Detection());
 
    }

    public IEnumerator Detection()
    {

        while (detecting)
        {           
            detectionsInColliders = 0;
            foreach(var colliderChance in colliderChances)
            { 
                if (colliderChance.IsInColliderWithChance())
                {
                    var tmp = colliderChance.ObjectsInCollider();
                    if (tmp != null)
                    {
                        var updateMyList = new List<GameObject>(tmp);
                        foreach (var e in tmp)
                        {
                            if (detections.Contains(e))
                            {
                                updateMyList.Remove(e);
                            }
                        }
                        if (updateMyList.Count > 0)
                        {
                            detections.AddRange(updateMyList);
                        }
                        detectionsInColliders++;
                    }
        
                }
                             
            }
            if (detectionsInColliders >= 1 && !alreadyDetect)
            {
                DetectingEvent();
                alreadyDetect = true;
            }
            else if(!isForgoting && forgeting)
            {
                isForgoting = true;
                Invoke("Forget", timeToForget);
            }
                     
            yield return new WaitForSeconds(timeToCheck);
        }
    }

    private void FixedUpdate()
    {
        if(alreadyDetect)
        {
            for (int i=0; i<colliderChances.Count; i++)
            {
                if(colliderChances[i].ObjectsInCollider()!=null)
                {
                    detectedColider = colliderChances[i];
                    break;
                }
            }         
        }
    }

    public void Forget()
    {
        if (detectionsInColliders < 1)
        {
            detections.Clear();
            alreadyDetect = false;
        }
        isForgoting = false;
    }

    private void OnDrawGizmos()
    {
        foreach(var e in colliderChances)
        {
            Gizmos.color = e.GizmosColor;
            Gizmos.DrawSphere(e.Center, e.Radius);
        }
    }

}

[System.Serializable]
public class ColliderChance
{
  
    [SerializeField] private float chance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Color gizmosColor;
    [SerializeField] private Transform center;
    [SerializeField] private float radius;
    [SerializeField] private AEnemyMovement.EnemyMoveType moveType;
    [SerializeField] private WeaponType weaponType;

    public AEnemyMovement.EnemyMoveType MoveType
    {
        get
        {
            return moveType;
        }
    }

    public WeaponType WeaponType
    {
        get
        {
            return weaponType;
        }
    }


    public Color GizmosColor
    {
        get
        {
            return gizmosColor;
        }
    }

    public Vector3 Center
    {
        get
        {
            return center.position;
        }
    }

    public float Radius
    {
        get
        {
            return radius;
        }
    }

    public bool IsInCollider()
    {

        var detect = Physics.CheckSphere(center.position, radius, layerMask, QueryTriggerInteraction.Ignore);
 
        return detect;
    }

    public bool IsInColliderWithChance()
    {
    
        if (IsInCollider())
        {
            var randome = Random.Range(0, 100);
            if (randome < chance)
            {
          
                return true;
            }
        }

        return false;
    }

    public List<GameObject> ObjectsInCollider()
    {
        if(IsInColliderWithChance())
        {

            var hits = Physics.SphereCastAll(center.position, radius, center.position,1, layerMask);
            List<GameObject> objects = new List<GameObject>();

            foreach(var hit in hits)
            {
                objects.Add(hit.collider.gameObject);
            }

            return objects;
        }
        return null;
    }


  

}


