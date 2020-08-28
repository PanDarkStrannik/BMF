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
                foreach (var v in spawner.spawned_objects)
                {
                    v.GetComponent<POPUP>().Init(e, damage);
                }
                break;
            }
        }        
        spawner.SpawnObject(damageble.gameObject.transform.position, Quaternion.identity);
    }



}

[System.Serializable]
public struct PopupData
{
    [SerializeField] private float minDamage;
    [SerializeField] private float speed;
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

    public float Speed
    {
        get
        {
            return speed;
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
