using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavMeshBaker : MonoBehaviour
{
    [SerializeReference] private List<NewDestruct> destructs;
    [SerializeReference] private List<NavMeshSurface> navMeshes;
    [SerializeField] private UnityEvent OnGenerateEnd;

    private void OnEnable()
    {
        foreach(var e in destructs)
        {
            e.OnDestruct.AddListener(GenerateMesh);
        }
    }
    private void OnDisable()
    {
        foreach (var e in destructs)
        {
            e.OnDestruct.RemoveListener(GenerateMesh);
        }
    }

    public void GenerateNavmeshByTime(float time)
    {
        StartCoroutine(GenerateMesh(time));
    }

    private IEnumerator GenerateMesh(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        foreach (var nav in navMeshes)
        {
            if (nav.gameObject.activeSelf)
            {
                nav.BuildNavMesh();
            }
        }
        OnGenerateEnd?.Invoke();
    }

}
