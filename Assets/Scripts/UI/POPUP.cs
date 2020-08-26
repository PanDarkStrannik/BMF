using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POPUP : MonoBehaviour
{
    [SerializeField] private float speed=1f;
    [SerializeField] private int speedMinim=1; 
    [SerializeField] private TextMesh textMesh;

    private GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.LookAt(player.transform);
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        textMesh.fontSize-=speedMinim;
    }
}
