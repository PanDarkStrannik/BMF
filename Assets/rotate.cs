using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float speed = 3;
    void Update()
    {
        Vector3 rotation = new Vector3(0, speed * Time.deltaTime, 0);
        transform.Rotate(rotation);
    }
}
