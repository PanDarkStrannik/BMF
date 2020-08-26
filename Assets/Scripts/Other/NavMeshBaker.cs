using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{

    [SerializeField] List<NavMeshSurface> navMeshes;
    [SerializeField] List<SimpleDestruct> simpleDestructs;

    private void Start()
    {
        foreach(var destrcut in simpleDestructs)
        {
            destrcut.DestructEvent += GenerateMesh;
        }
    }

    public void GenerateMesh()
    {
        foreach(var nav in navMeshes)
        {
            nav.BuildNavMesh();
        }
    }

}
