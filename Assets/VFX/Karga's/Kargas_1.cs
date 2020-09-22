using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kargas_1 : MonoBehaviour
{
    public float scale = 0;
    private Vector3 ScaleChange;
    public float scaleMultiplier = 0.1f;

    private void Awake()
    {
        ScaleChange = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);
    }

    void Update()
    {
        if (scale < 0.5f)
        {
            scale += scaleMultiplier * Time.deltaTime;
            transform.localScale += ScaleChange * Time.deltaTime;
        }
    }
}
