using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveSphere : MonoBehaviour
{
    public GameObject sphere;
    public float DisolveAmount;
    private MeshRenderer meshRenderer;
    private float time;

    private void Start()
    {
        time = 1;
        meshRenderer = GetComponent<MeshRenderer>();

    }

    private void Update()
    {
        
        meshRenderer.material.SetFloat("_DisolveAmount", DisolveAmount);
        if (time <= 0)
        {
            if (DisolveAmount > -0.9) DisolveAmount -= Time.deltaTime;
            else
            {
                Destroy(gameObject);
                Instantiate(sphere, transform.position, Quaternion.identity);
            }
        }
        else time -= Time.deltaTime;
    }
}
