using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displacement : MonoBehaviour
{
    public float DisplacementAmount;
    public MeshRenderer meshRenderer;
    private float time;

    private void Start()
    {
        time = 1;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        DisplacementAmount = Mathf.Lerp(DisplacementAmount, 0, Time.deltaTime);
        meshRenderer.material.SetFloat("_Amount", DisplacementAmount);

        if (time <= 0)
        {
            time = 1;
            DisplacementAmount += 0.3f;
        }
        else time -= Time.deltaTime;
    }
}
