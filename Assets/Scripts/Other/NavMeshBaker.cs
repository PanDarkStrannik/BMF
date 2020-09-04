using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{

    [SerializeField] List<NavMeshSurface> navMeshes;
    [SerializeField] List<SimpleDestruct> simpleDestructs;
    [SerializeField] private float toBakeTime = 3f;

    private void Start()
    {
        foreach(var destrcut in simpleDestructs)
        {
            destrcut.DestructEvent += delegate
            {
                StartCoroutine(GenerateMesh());
            };
        }
    }

    public IEnumerator GenerateMesh()
    {
        yield return new WaitForSecondsRealtime(toBakeTime);
        foreach(var nav in navMeshes)
        {
            if (nav.gameObject.activeSelf)
            {
                nav.BuildNavMesh();
            }
        }
    }

}
