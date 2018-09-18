using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionTDWave : MonoBehaviour 
{
    #region Events

    public event EventHandler WaveStarted;

    public event EventHandler WaveEnded;

    #endregion
    
    [SerializeField]
    public List<RedemptionTDSpawnInfo> SpawnInformation;

    [SerializeField]
    public bool StaggeredSpawning;

    private RedemptionEnemyObjectPool objectPool;

    public void StartWave(RedemptionEnemyObjectPool pool)
    {
        if(objectPool == null)
        {
            objectPool = pool;
        }
        
        if(StaggeredSpawning)
        {
            StaggeredSpawn();
        }
        else
        {
            SimultaneousSpawn();
        }
    }

    private void StaggeredSpawn()
    {
        StartCoroutine(StaggeredSpawnRoutine());
    }

    private void SimultaneousSpawn()
    {
        StartCoroutine(SimultaneousSpawnRoutine());
    }

    private IEnumerator StaggeredSpawnRoutine()
    {
        RaiseWaveStart();
        foreach(var spawnInfo in SpawnInformation)
        {
            yield return StartCoroutine(SpawnEnemiesRoutine(spawnInfo));
        }
    }

    private IEnumerator SimultaneousSpawnRoutine()
    {
        RaiseWaveStart();
        foreach(var spawnInfo in SpawnInformation)
        {
            StartCoroutine(SpawnEnemiesRoutine(spawnInfo));
        }

        yield return null;
    }

    private IEnumerator SpawnEnemiesRoutine(RedemptionTDSpawnInfo spawnInfo)
    {
        yield return new WaitForSeconds(spawnInfo.StartSpawnDelay);

        var enemies = objectPool.GetEnemies(spawnInfo.EnemyType, spawnInfo.NumberToSpawn);
        foreach(var enemy in enemies)
        {
            var movement = enemy.GetComponent<EnemyMovement>();
            movement.Initialize(spawnInfo.SpawnPosition);
            movement.Move();
            yield return new WaitForSeconds(spawnInfo.TimeBetweenSpawns);
        }

        yield return new WaitForSeconds(spawnInfo.StopSpawnDelay);
    }

    public void RaiseWaveStart()
    {
        var handler = WaveStarted;
        if(handler != null)
        {
            handler(this, null);
        }

        Debug.Log("Wave Started");
    }

    public void RaiseWaveEnd()
    {
        var handler = WaveEnded;
        if(handler != null)
        {
            handler(this, null);
        }

        Debug.Log("Wave Ended");
    }
}
