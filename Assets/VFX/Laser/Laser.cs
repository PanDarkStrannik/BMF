using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private LineRenderer laser;
    private float plus;

    void Update()
    {
        plus = 7 * Time.deltaTime;

        if (laser.GetPosition(1).z < 3)
        {
            laser.SetPosition(1, new Vector3(0, 0, laser.GetPosition(1).z + plus));
        }

        if (time <= 0)
        {
            if (laser.GetPosition(0).z < 3)
            {
                laser.SetPosition(0, new Vector3(0, 0, laser.GetPosition(0).z + plus));
            }

            else Destroy(gameObject, 1);
        }
        else time -= Time.deltaTime;
    }
}
