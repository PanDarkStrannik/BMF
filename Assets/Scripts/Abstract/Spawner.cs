using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Spawner
{
    [SerializeField]
    [Tooltip("Лист префабов для спавна")]
    private List<GameObject> spawnObjects;

    [SerializeField]
    [Tooltip("Лист колличества префабов спавна")]
    private List<int> objectsValues;

    [HideInInspector] public List<GameObject> spawned_objects;

    private Queue<GameObject> _objects_queue;
    private Dictionary<GameObject, int> _object_value_pair;

    /// <summary>
    /// Пул объектов (не используется)
    /// </summary>
    protected GameObject QueueObject
    {
        get
        {
            return _objects_queue.Dequeue();
        }
        set
        {
            _objects_queue.Enqueue(value);
        }
    }

    #region Виртуальные методы

    public virtual void CreateSpawner()
    {
        _object_value_pair = new Dictionary<GameObject, int>();
        spawned_objects = new List<GameObject>();
        _objects_queue = new Queue<GameObject>();

        for (int i = 0; i < spawnObjects.Count; i++)
        {
            _object_value_pair.Add(spawnObjects[i], objectsValues[i]);
            for (int j = 0; j < objectsValues[i]; j++)
            {
                GameObject created_object = MonoBehaviour.Instantiate(spawnObjects[i]);
                _objects_queue.Enqueue(created_object);
                spawned_objects.Add(created_object);
                created_object.SetActive(false);
            }
        }
        
    }


    protected virtual void SpawnObject(Vector3 position, Quaternion rotation, GameObject spawned_object)
    {
        spawned_object.transform.position = position;
        spawned_object.transform.rotation = rotation;
        spawned_object.SetActive(true);
    }

    #endregion




    #region Собственные наследуемые методы

    public void SpawnObject(Vector3 position, Quaternion rotation)
    {
        if (_objects_queue.Count > 0)
        {
            SpawnObject(position, rotation, _objects_queue.Dequeue());
        }
    }


    public void ReturnObject(GameObject returnedObject)
    {       
        foreach(var spawned_object in spawned_objects)
        {
            if (spawned_object.GetInstanceID() == returnedObject.GetInstanceID())
            {
                _objects_queue.Enqueue(returnedObject);
                returnedObject.SetActive(false);
                break;
            }
        }
    }

    #endregion


}
