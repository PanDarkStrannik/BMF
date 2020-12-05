using Scripts.DevelopingSupporting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameEvents : SupportingScripts.MonoBehSinglton<GlobalGameEvents>
{
    public delegate void OnEnemySpawnStartHandler();
    public event OnEnemySpawnStartHandler OnEnemySpawnStart;  

    public void EnemySpawnStart()
    {
        OnEnemySpawnStart?.Invoke();
    }

}
