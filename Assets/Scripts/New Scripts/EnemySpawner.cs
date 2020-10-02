using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private List<SpawnStages> spawnStages;
    [SerializeField] private int testPoint=1;
    [SerializeField] private Transform spawnPosition;

    private SpawnStages currentStage;

    private void Awake()
    {
        spawner.CreateSpawner();
        spawnStages.OrderBy(stage => stage.StagePoint);
    }

    private void Start()
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

    private void Update()
    {
        ChangeStage(testPoint);     
    }

    private void ChangeStage(int point)
    {
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
            for (int i = 0; i < currentStage.EnemyValue; i++)
            {
                spawner.SpawnObject(spawnPosition.position, spawnPosition.rotation);
            }
            yield return new WaitForSeconds(currentStage.TimeBetweenSpawn);
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
