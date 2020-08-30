using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super : MonoBehaviour
{
    Rigidbody rb;
    float start = 2;
    bool started = false;
    [SerializeField] ParticleSystem ps;
    [SerializeField] ParticleSystem ps2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start <= 0 && !started)
        {
            started = true;
            rb.AddForce(transform.forward * -1000);
        }
        else
        {
            start -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            Instantiate(ps, transform.position, Quaternion.identity);
            ps2.Stop();
            Destroy(gameObject);
        }
    }
}
