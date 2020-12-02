using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestruct : MonoBehaviour
{
    [SerializeField] DamagebleParamSum paramSum;
    [SerializeField] List<DamagebleParam.ParamType> paramTypes;
    [SerializeField] List<Rigidbody> objectsForForce;
    [SerializeField] List<GameObject> objectsForDeactive;
    [SerializeField] float timeToDeactive=1f;
    [SerializeField] float explousionForce = 1f;
    [SerializeField] float explousionRadius = 1f;
    [SerializeField] Transform forceTransform;

    [SerializeField] private List<SimpleDestruct> destructs;

    [SerializeField] private int minCountToDestruct=0;

    private int countDestruct = 0;
    private bool isDestruct;


    public delegate void Destruct();
    public event Destruct DestructEvent;

    private void OnEnable()
    {
        isDestruct = false;
        paramSum.Initialize();

        paramSum.OnParamNull += CheckType;

        foreach(var destruct in destructs)
        {
            destruct.DestructEvent += delegate
            {
                countDestruct++;
                if (countDestruct >= minCountToDestruct)
                {
                    DestructObjects();
                }
            };
        }
    }

    private void OnDisable()
    {
        paramSum.Unsubscribe();
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
        foreach (var obj in objectsForForce)
        {
            obj.isKinematic = false;
            obj.AddExplosionForce(explousionForce, forceTransform.position, explousionRadius);
        }

        Invoke("ToDeactive", timeToDeactive);
    }

    private void ToDeactive()
    {
        foreach(var obj in objectsForDeactive)
        {
            obj.SetActive(false);
        }
        DestructEvent?.Invoke();
    }

}
