using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvePS : MonoBehaviour
{
    public Material material;
    private float dissolveAmount = 2;
    public float step = 0.05f;
    private bool active = false;

    private void Awake()
    {
        active = true;
    }
    private void Update()
    {
        if (dissolveAmount > -2 && active == true)
        {
            dissolveAmount -= step * Time.deltaTime;
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }
}
