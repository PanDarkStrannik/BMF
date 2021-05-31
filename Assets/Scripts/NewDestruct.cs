using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class NewDestruct : MonoBehaviour
{
    [SerializeField] DamagebleParamSum paramSum;
    [SerializeField] List<DamagebleParam.ParamType> paramTypes;
    [SerializeField] private List<RigidbodyForceComparer> rigidbodysForcesComparer = new List<RigidbodyForceComparer>();

    [SerializeField] private List<NewDestruct> destructs;

    [SerializeField] private int minCountToDestruct = 0;
    public UnityEvent OnDestruct;

    private int countDestruct = 0;
    private bool isDestruct;


    private void OnEnable()
    {
        isDestruct = false;
        paramSum.Initialize();

        paramSum.OnParamNull += CheckType;

        foreach (var destruct in destructs)
        {
            // null refernce \/
          //  destruct.OnDestruct.AddListener(DestructEventListener);
        }
    }

    private void DestructEventListener()
    {
        countDestruct++;
        if (countDestruct >= minCountToDestruct)
        {
            DestructObjects();
        }
    }

    private void OnDisable()
    {
        paramSum.Unsubscribe();

        paramSum.OnParamNull -= CheckType;

        foreach (var destruct in destructs)
        {
            destruct.OnDestruct.RemoveListener(DestructEventListener);
        }

        paramSum.SetDefault();
    }


    private void CheckType(DamagebleParam.ParamType type)
    {
        if (paramTypes.Count > 0)
        {
            foreach (var myType in paramTypes)
            {
                if (type == myType && !isDestruct)
                {
                    isDestruct = true;
                    DestructObjects();
                    break;
                }
            }
        }
    }

    private void DestructObjects()
    {
        if (rigidbodysForcesComparer.Count > 0)
        {
            rigidbodysForcesComparer.OrderBy(x => x.DestructNumber);
            rigidbodysForcesComparer.Reverse();
            foreach (var e in rigidbodysForcesComparer)
            {
                e.EnableRigidbody();
            }
            for (int i = 0; i < rigidbodysForcesComparer.Count; i++)
            {
                rigidbodysForcesComparer[i].AddForce();
            }
        }
        OnDestruct?.Invoke();
    }


    private void OnDrawGizmosSelected()
    {
        if (rigidbodysForcesComparer.Count > 0)
        {
            foreach (var e in rigidbodysForcesComparer)
            {
                e.DrawGizmos();
            }
        }
    }

    [System.Serializable]
    private struct RigidbodyForceComparer
    {
        [SerializeField,Min(0)] private int destructNumber;
        [SerializeReference] private Rigidbody objectForForce;
        [SerializeField] private Vector3 forceVector;
        [SerializeField] private ForceMode forceMode;
        [SerializeField] private Color color;
        [SerializeField,Min(0)] private float gizmosRadius;
        [SerializeField] private UnityEvent ForceAdded;

        public int DestructNumber
        {
            get
            {
                return destructNumber;
            }
        }

        public void DrawGizmos()
        {
            if(objectForForce!=null)
            {
                Gizmos.color = color;
                Gizmos.DrawSphere(objectForForce.position, gizmosRadius);
                Gizmos.DrawLine(objectForForce.position, objectForForce.position + forceVector);
                Gizmos.DrawSphere(objectForForce.position + forceVector, gizmosRadius / 2);
            }
        }

        public void AddForce()
        {
            EnableRigidbody();
            objectForForce.AddForce(forceVector, forceMode);
            ForceAdded?.Invoke();
        }

        public void DisableRigidBody()
        {
            objectForForce.isKinematic = true;
        }

        public void EnableRigidbody()
        {
            objectForForce.isKinematic = false;
        }
    }
}
