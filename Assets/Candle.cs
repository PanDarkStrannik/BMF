using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] private GameObject[] disableObj;

    private void DisableObjects()
    {
        foreach (var o in disableObj)
        {
            o.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            DisableObjects();
        }
    }
}
