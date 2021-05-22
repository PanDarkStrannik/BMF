using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class SummoningWeapon : AWeapon, IDamagingWeapon
{
    [SerializeField] private Spawner spawner;

    [SerializeField] private List<EnemySpawnChances> enemyChances;

    [Range(0,100)]
    [SerializeField] private float multiplieChance = 0f;

    [SerializeReference] private Transform spawnPoint;

    [SerializeField] private float timeToSpawn = 0f;
    [SerializeField] private float reloadTime = 5f;

    [SerializeField] private UnityEvent<bool> attackStart;
    [SerializeField] private UnityEvent attack;

    private bool isReadyToSummon;

    public override WeaponType WeaponType
    {
        get
        {
            return WeaponType.Summon;
        }
    }

    private void Start()
    {
        enemyChances = enemyChances.OrderBy(enemyChances => enemyChances.Chance).ToList();
        spawner.CreateSpawner();
        isReadyToSummon = true;
    }

    public override void UseWeapon()
    {
        Attack();
    }

    public void Attack()
    {
        if (State == WeaponState.Serenity && isReadyToSummon)
        {
            StartCoroutine(MultiplieSummonSpawn(timeToSpawn));
        }
    }

    private IEnumerator MultiplieSummonSpawn(float time)
    {
        State = WeaponState.Attack;
        attackStart?.Invoke(true);
        yield return new WaitForSeconds(time);
        attack?.Invoke();

        var rand = Random.Range(0, 100);
        if (rand < multiplieChance)
        {
            //SpawnByLayer(ReturnSomeLayer());
            SpawnGameObject(ReturnSomeGameObject());
            MultiplieSummonSpawn(timeToSpawn);
        }
        else
        {
            SpawnGameObject(ReturnSomeGameObject());
            //SpawnByLayer(ReturnSomeLayer());
        }

        StartCoroutine(Reload(reloadTime));
        attackStart?.Invoke(false);
    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;

        if(State == WeaponState.Reload)
        {
            isReadyToSummon = false;
            yield return new WaitForSeconds(time);
            StartCoroutine(Serenity(0));
        }
    }

    protected override IEnumerator Serenity(float time)
    {
        if(State != WeaponState.Serenity)
        {
            yield return new WaitForSeconds(time);
            State = WeaponState.Serenity;
            isReadyToSummon = true;
            StopAllCoroutines();
        }
    }

    #region Старые функции
    //private LayerMask ReturnSomeLayer()
    //{

    //    if (enemyChances.Count > 0)
    //    {
    //        var rand = Random.Range(0, 100);

    //        for (int i = 0; i < enemyChances.Count; i++)
    //        {
    //            if (rand < enemyChances[i].Chance)
    //            {
    //                return enemyChances[i].GameObject.layer;
    //            }
    //        }

    //        throw new System.Exception($"Для выпавшего рандома {rand} нет шансов для спавна");
    //    }
    //    else
    //    {
    //        throw new System.Exception("Нет врагов и шансов для спавна!");
    //    }
    //}

    //private void SpawnByLayer(LayerMask layer)
    //{
    //    var tempSpawnedObjects = spawner.spawned_objects;
    //    foreach (var spawnObject in tempSpawnedObjects)
    //    {
    //        if (layer == spawnObject.layer)
    //        {
    //            //spawner.SpawnObjectFromQueue(spawnPoint.position, spawnPoint.rotation, spawnObject);
    //            if (spawner.TryReturnFamiliarObject(spawnObject))
    //            {
    //                spawner.SpawnObject(spawnPoint.position, spawnPoint.rotation, spawner.ReturnFamiliarObject(spawnObject));
    //                //return true;
    //                break;
    //            }
    //        }
    //    }
    //    //return false;
    //}
    #endregion

    private GameObject ReturnSomeGameObject()
    {

        if (enemyChances.Count > 0)
        {
            var rand = Random.Range(0, 100);
            Debug.Log($"Выпавший рандом: {rand}");

            for (int i = 0; i < enemyChances.Count; i++)
            {
                if (rand < enemyChances[i].Chance)
                {
                    Debug.Log("Рандом прошёл");
                    return enemyChances[i].GameObject;
                }
            }

            throw new System.Exception($"Для выпавшего рандома {rand} нет шансов для спавна");
        }
        else
        {
            throw new System.Exception("Нет врагов и шансов для спавна!");
        }
    }

    private void SpawnGameObject(GameObject gameObject)
    {
        var tempSpawnedObjects = spawner.spawned_objects;
        foreach (var spawnObject in tempSpawnedObjects)
        {
             spawner.SpawnFirstObjectInQueue(spawnPoint.position, spawnPoint.rotation);
            if (spawner.TryReturnFamiliarObject(gameObject))
            {
                spawner.SpawnObject(spawnPoint.position, spawnPoint.rotation, spawner.ReturnFamiliarObject(spawnObject));
                //return true;
                break;
            }
        }

        if (spawner.TryReturnFamiliarObject(gameObject))
        {
            Debug.Log("Данный объект можно заспавнить!");
            spawner.SpawnObject(spawnPoint.position, spawnPoint.rotation, spawner.ReturnFamiliarObject(gameObject));
        }
        else
        {
            Debug.Log("Что-то пошло не так!");
        }
    }



    [System.Serializable]
    private class EnemySpawnChances
    {
        [SerializeField] private GameObject gameObject;
        [Range(0,100)]
        [SerializeField] private int chance;

        public GameObject GameObject
        {
            get
            {
                return gameObject;
            }
        }

        public int Chance
        {
            get
            {
                return chance;
            }
        }
    }

}
