using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disolveAmoint : MonoBehaviour
{
    public Material material;
    private float dissolveAmount = -2;
    public float step = 0.05f;

    private void Update()
    {
        if (dissolveAmount < 2)
        {
            dissolveAmount += step * Time.deltaTime;
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }
}
