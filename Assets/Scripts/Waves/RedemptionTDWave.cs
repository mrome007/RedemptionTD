using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private RedemptionTDObjectPool objectPool;
    private int currentSpawnCount;
    private int totalSpawnCount;
    private List<LiteUnit> currentSpawns;

    public void StartWave(RedemptionTDObjectPool pool)
    {
        if(currentSpawns == null)
        {
            currentSpawns = new List<LiteUnit>();
        }

        currentSpawnCount = 0;
        totalSpawnCount = SpawnInformation.Sum(spawnInfo => spawnInfo.NumberToSpawn);
            
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

        var enemies = objectPool.GetUnits(spawnInfo.EnemyType, spawnInfo.NumberToSpawn);
        foreach(var enemy in enemies)
        {
            enemy.SpawnObject(currentSpawnCount, spawnInfo.SpawnPosition.transform.position);
            enemy.ObjectReturned += HandleEnemyReturned;

            var movement = enemy.GetComponent<EnemyMovement>();

            movement.Initialize(spawnInfo.SpawnPosition);
            movement.Move();

            if(currentSpawnCount < currentSpawns.Count)
            {
                currentSpawns[currentSpawnCount] = enemy;
            }
            else
            {
                currentSpawns.Add(enemy);
            }

            currentSpawnCount++;

            yield return new WaitForSeconds(spawnInfo.TimeBetweenSpawns);
        }
       
        yield return new WaitForSeconds(spawnInfo.StopSpawnDelay);
    }

    private void SpawnResourceDrops(LiteUnit liteUnit, Vector3 pos)
    {
        var enemy = liteUnit.HeavyReference as Enemy;
        if(enemy == null)
        {
            return;
        }

        var numToSpawn = UnityEngine.Random.Range(0, enemy.MaxResourceDrops);
        var units = objectPool.GetUnits(objectPool.GetResourceDropType(enemy.Color), numToSpawn);
        var count = 0;
        foreach(var resourceDrop in units)
        {
            var position = new Vector3(pos.x + UnityEngine.Random.Range(-0.1f, 0.1f), 
                                       pos.y + UnityEngine.Random.Range(-0.1f, 0.1f), 
                                       -1f);
            resourceDrop.SpawnObject(count, position);
            count++;
        }
    }

    private void HandleEnemyReturned(object sender, ToOrFromPoolEventArgs e)
    {
        currentSpawns[e.SpawnIndex].ObjectReturned -= HandleEnemyReturned;
        totalSpawnCount--;

        var liteUnit = sender as LiteUnit;
        if(e.Dead && liteUnit != null)
        {
            SpawnResourceDrops(liteUnit, e.LastPosition);
        }

        if(totalSpawnCount <= 0)
        {
            RaiseWaveEnd();
        }
    }

    public void RaiseWaveStart()
    {
        Debug.Log("Wave Started");

        var handler = WaveStarted;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    public void RaiseWaveEnd()
    {
        Debug.Log("Wave Ended");

        var handler = WaveEnded;
        if(handler != null)
        {
            handler(this, null);
        }
    }
}
