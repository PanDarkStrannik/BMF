﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Spawner enemySpawner;
    [SerializeField] private List<SpawnStages> spawnStages;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private UnityEvent spawnEvent;
    [SerializeField] private UnityEvent<bool> spawnStart;
    [SerializeField] private float toSpawnTime;

    private bool firstInit = false;

    private SpawnStages currentStage;

    private void Awake()
    {
        //enemySpawner.CreateSpawner();
        //spawnStages.OrderBy(stage => stage.StagePoint);
        //foreach (var enemy in enemySpawner.spawned_objects)
        //{
         //  enemy.GetComponentInChildren<EnemyParamController>().OnEnemyDie += delegate { enemySpawner.ReturnObject(enemy); };
        //}
    }

    private void Start()
    {

        enemySpawner.CreateSpawner();
        spawnStages.OrderBy(stage => stage.StagePoint);
        foreach (var enemy in enemySpawner.spawned_objects)
        {
            //enemy.GetComponentInChildren<EnemyParamController>().OnEnemyDie += delegate { enemySpawner.ReturnObject(enemy); };
        }

        Subscribe();
        
        firstInit = true;
    }


    private void StartSpawn()
    {
        if (spawnStages.Count > 0)
        {
            currentStage = spawnStages[0];
        }
        else
        {
            throw new System.Exception("Не введён лист спавна врагов");
        }

        StartCoroutine(SpawnBetweenTime());
    }

    private void Subscribe()
    {
        GlobalGameEvents.Instance.OnEnemySpawnStart += StartSpawn;
        PointCounter.Instance.PointEvent += ChangeStage;
    }

    private void Unsubscribe()
    {
        GlobalGameEvents.Instance.OnEnemySpawnStart -= StartSpawn;
        StopCoroutine(SpawnBetweenTime());
        PointCounter.Instance.PointEvent -= ChangeStage;
    }

    private void OnEnable()
    {
        if (firstInit == true)
        {
            Subscribe();
        }
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    //private void OnDestroy()
    //{
    //    GlobalGameEvents.Instance.OnEnemySpawnStart -= StartSpawn;
    //    StopCoroutine(SpawnBetweenTime());
    //}

    private void ChangeStage(int point)
    {
        Debug.Log("Текущие очки: " + point);

        foreach (var stage in spawnStages)
        {
            if (stage.StagePoint == point)
            {
                currentStage = stage;
                break;
            }
        }
    }

    private IEnumerator SpawnBetweenTime()
    {
        while(true)
        {
            spawnEvent?.Invoke();
            spawnStart?.Invoke(true);
            yield return new WaitForSeconds(toSpawnTime);
            for (int i = 0; i < currentStage.EnemyValue; i++)
            {
                enemySpawner.SpawnFirstObjectInQueue(spawnPosition.position, spawnPosition.rotation);                
            }
            yield return new WaitForSeconds(currentStage.TimeBetweenSpawn);
            spawnStart?.Invoke(false);
        }
    }


    [System.Serializable]
    private struct SpawnStages
    {
        [SerializeField] private string stageName;
        [SerializeField] private float enemyValue;
        [SerializeField] private float timeBetweenSpawn;
        [SerializeField] private int stagePoint;

        public string StageName
        {
            get
            {
                return stageName;
            }
        }

        public float EnemyValue
        {
            get
            {
                return enemyValue;
            }
        }

        public float TimeBetweenSpawn
        {
            get
            {
                return timeBetweenSpawn;
            }
        }

        public int StagePoint
        {
            get
            {
                return stagePoint;
            }
        }
    }

   
}
