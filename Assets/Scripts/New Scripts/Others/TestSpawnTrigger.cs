using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TestSpawnTrigger : SimpleTrigger
{
    [SerializeField] private MeshRenderer meshRender;
    [SerializeField] private UnityEvent TriggerExit;


    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        meshRender.material.color = Color.green;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other != null)
        {
            meshRender.material.color = Color.red;
            TriggerExit?.Invoke();
        }
    }


}
