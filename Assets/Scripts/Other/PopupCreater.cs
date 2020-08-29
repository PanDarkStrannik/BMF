using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCreater : MonoBehaviour
{
    [SerializeField] private Spawner spawner;

    [SerializeField] private List<ADamageble> damagebles;

    [SerializeField] private List<PopupData> popupValueColors;
    

    private void Awake()
    {
        spawner.CreateSpawner();
        foreach (var e in damagebles)
        {
            e.OnDamagedWithValue += CreatePopup;
        }
    }
    private void OnEnable()
    {
        foreach (var e in spawner.spawned_objects)
        {
            e.GetComponent<POPUP>().OnPopupDie += spawner.ReturnObject;
        }
    }

    //private void OnDisable()
    //{
    //    foreach (var e in spawner.spawned_objects)
    //    {
    //        e.GetComponent<POPUP>().OnPopupDie -= spawner.ReturnObject;
    //    }
    //}

    //private void OnDestroy()
    //{
    //    foreach (var e in spawner.spawned_objects)
    //    {
    //        e.GetComponent<POPUP>().OnPopupDie -= spawner.ReturnObject;
    //    }
    //}

    private void CreatePopup(float damage, ADamageble damageble)
    {
        foreach (var e in popupValueColors)
        {
            if (damage < e.MinDamage)
            {
                for (int i = 0; i < spawner.spawned_objects.Count; i++)
                {
                    var tmp = spawner.QueueObject;
                    tmp.GetComponent<POPUP>().Init(e, damage);
                    spawner.QueueObject = tmp;
                }
                break;
            }
        }
        spawner.SpawnObject(damageble.gameObject.transform.position, Quaternion.identity);
        Debug.Log("Заспавнили попуп " + damage);
    }



}

[System.Serializable]
public struct PopupData
{
    [SerializeField] private float minDamage;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float timeToChangeSize;
    [SerializeField] private Color color;
    [SerializeField] private int maxSize;
    [SerializeField] private int minSize;
    [SerializeField] private int minimizeSpeed;

    public float MinDamage
    {
        get
        {
            return minDamage;
        }
    }

    public Color Color
    {
        get
        {
            return color;
        }
    }

    public float MinSpeed
    {
        get
        {
            return minSpeed;
        }
    }

    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }
    }

    public float TimeToChangeSize
    {
        get
        {
            return timeToChangeSize;
        }
    }
    
    public int MaxSize
    {
        get
        {
            return maxSize;
        }
    }

    public int MinSize
    {
        get
        {
            return minSize;
        }
    }

    public int MinimizeSpeed
    {
        get
        {
            return minimizeSpeed;
        }
    }
}
