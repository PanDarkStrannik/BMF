using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Scripts.DevelopingSupporting
{
    public static class SupportingScripts
    {
        public static class CastHelper
        {
            public static bool SpherecastInIerarhiy<T>(out RaycastHit hitWithComponent, Ray ray, float maxDistance, LayerMask layerMask)
            {
                if (Physics.SphereCast(ray, 1f, out RaycastHit hit, maxDistance, layerMask))
                {
                    hitWithComponent = hit;
                    if (hit.transform.GetComponentInParent(typeof(T)))
                    {
                        Debug.Log("У родителя нашли компонент!");
                        return true;
                    }
                    else if (hit.transform.GetComponentInChildren(typeof(T)))
                    {
                        Debug.Log("У ребёнка нашли компонент!");
                        return true;
                    }
                    else if (hit.transform.GetComponent(typeof(T)))
                    {
                        Debug.Log("У самого объекта компонент!");
                        return true;
                    }
                    else
                    {
                        Debug.Log("Не нашли компонент!");
                        return false;
                    }
                }
                hitWithComponent = hit;
                return false;

            }


        }

        public static class MathHelper
        {
            public static Vector3 ClampVector(Vector3 currentVector, Vector3 clampingMin, Vector3 clampingMax)
            {
                var clamped = new Vector3();
                clamped.x = Mathf.Clamp(currentVector.x, clampingMin.x, clampingMax.x);
                clamped.y = Mathf.Clamp(currentVector.y, clampingMin.y, clampingMax.y);
                clamped.z = Mathf.Clamp(currentVector.z, clampingMin.z, clampingMax.z);

                return clamped;
            }

            public static float ClampAngle(float angle, float min, float max)
            {
                if (angle < -360F) angle -= 360F;
                if (angle > 360F) angle += 360F;
                return Mathf.Clamp(angle, min, max);
            }

        }



        public static class PauseController
        {
            public static bool isPaused = false;

            /// <summary>
            /// Поставить на паузу
            /// </summary>
            public static void Pause()
            {
                if (!isPaused)
                {
                    Time.timeScale = 0;
                    isPaused = true;
                }
            }

            /// <summary>
            /// Снять с паузы
            /// </summary>
            public static void Resume()
            {
                if (isPaused)
                {
                    Time.timeScale = 1;
                    isPaused = false;
                }
            }

            /// <summary>
            /// Выход из игры
            /// </summary>
            public static void Quit()
            {
                Application.Quit();
            }

            /// <summary>
            /// Рестарт сцены
            /// </summary>
            public static void Restart()
            {
                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.buildIndex);
            }


            /// <summary>
            /// Изменяет время игры на указанное
            /// </summary>
            /// <param name="value">Желаемое време</param>
            public static void ChangeTime(float value)
            {
                Time.timeScale = value;
            }

        }


        public class Singlton<T> where T : Singlton<T>, new()
        {
            private static T instance;
            private static object synchRoot = new object();

            public static T Instance
            {
                get
                {
                    lock (synchRoot)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }
                    return instance;
                }
                private set
                {
                    instance = value;
                }
            }
               

            protected Singlton()
            {
                Creating();
            }

            protected virtual void Creating()
            {

            }
        }

        public class MonoBehSinglton<T> : MonoBehaviour where T : MonoBehSinglton<T>, new()
        {


            public static T Instance
            {
                get; private set;
            }
       

            protected virtual void Creating()
            {
               
            }

            private void OnEnable()
            {
                if (Instance == null)
                {
                    Instance = this as T;
                    Creating();
                }

            }

            private void OnDisable()
            {
                Instance = null;
            }




        }

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
            public GameObject QueueObject
            {
                get
                {
                    if (_objects_queue.Count > 0)
                    {
                        return _objects_queue.Dequeue();
                    }
                    return null;
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
                        if (created_object.GetComponent<SpawnedObject>() != null)
                        {
                            created_object.GetComponent<SpawnedObject>().OnDieEvent += ReturnObject;
                        }
                        else
                        {
                            throw new System.Exception("На объекте для спавна нет компонента SpawnedObject!");
                        }
                    }
                }

            }


            public virtual void SpawnObject(Vector3 position, Quaternion rotation, GameObject spawned_object)
            {
                spawned_object.transform.position = position;
                spawned_object.transform.rotation = rotation;
                spawned_object.SetActive(true);
            }

            #endregion




            #region Собственные наследуемые методы

            public void SpawnFirstObjectInQueue(Vector3 position, Quaternion rotation)
            {
                if (_objects_queue.Count > 0)
                {
                    SpawnObject(position, rotation, _objects_queue.Dequeue());
                }
                else
                {
                    throw new System.Exception("Куча пуста!");
                }
            }


            public GameObject ReturnFamiliarObject(GameObject gameObject)
            {
                if (_objects_queue.Count > 0)
                {
                    if (gameObject.GetComponent<SpawnedObject>() != null)
                    {

                        var tempQueueList = new List<GameObject>(_objects_queue);

                        for (int i = 0; i < tempQueueList.Count; i++)
                        {
                            if (gameObject.GetComponent<SpawnedObject>().ID == tempQueueList[i].GetComponent<SpawnedObject>().ID)
                            {
                                _objects_queue.Clear();
                                var temp = tempQueueList[i];
                                tempQueueList.Remove(tempQueueList[i]);
                                _objects_queue = new Queue<GameObject>(tempQueueList);
                                return temp;
                            }
                        }
                        throw new System.Exception("Ничего не могу вернуть");
                    }
                    else
                    {
                        throw new System.Exception("На желанном для спавна объекте нет скрипта SpawnedObject!");
                    }
                }
                else
                {
                    throw new System.Exception("Куча пуста!");
                }
            }

            public bool TryReturnFamiliarObject(GameObject gameObject)
            {
                if (_objects_queue.Count > 0)
                {
                    if (gameObject.GetComponent<SpawnedObject>() != null)
                    {
                        var tempQueueList = new List<GameObject>(_objects_queue);
                        for (int i = 0; i < tempQueueList.Count; i++)
                        {
                            if (gameObject.GetComponent<SpawnedObject>().ID == tempQueueList[i].GetComponent<SpawnedObject>().ID)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        throw new System.Exception("На желанном для спавна объекте нет скрипта SpawnedObject!");
                    }
                }
                else
                {
                    throw new System.Exception("Куча пуста!");
                }
            }

            private void ReturnObject(GameObject returnedObject)
            {
                foreach (var spawned_object in spawned_objects)
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

    }
}
