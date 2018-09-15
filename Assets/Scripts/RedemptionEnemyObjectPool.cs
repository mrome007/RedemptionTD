using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionEnemyObjectPool : MonoBehaviour 
{
    #region Public Data

    public event EventHandler ObjectPoolBegin;

    public event EventHandler ObjectPoolComplete;

    #endregion
    
    #region Inspector Data

    [SerializeField]
    private List<RedemptionEnemy> enemyList;

    [SerializeField]
    private EnemyHeavyReferences heavyReferences;

    [SerializeField]
    private Transform poolParent;

    #endregion

    #region Private Data

    private Dictionary<RedemptionEnemyType, EnemyLite> enemyDictionary;
    private Dictionary<RedemptionEnemyType, List<EnemyLite>> enemyPool;
    private Dictionary<RedemptionEnemyType, int> enemyPoolIndex;

    #endregion

    private void Awake()
    {
        InitializeEnemyDictionary();
        CreateRedemptionObjectPool();
    }

    public IEnumerable<EnemyLite> GetEnemies(RedemptionEnemyType enemyType, int numberOfEnemies)
    {
        for(int count = 0; count < numberOfEnemies; count++)
        {
            if(!enemyPool.ContainsKey(enemyType))
            {
                break;
            }
                
            var enemyPoolList = enemyPool[enemyType];
            var enemyIndex = enemyPoolIndex[enemyType];

            if(enemyIndex < enemyPoolList.Count)
            {
                var enemy = enemyPoolList[enemyIndex];
                enemyPoolList[enemyIndex] = null;
                enemyPoolIndex[enemyType]++;
                enemy.gameObject.SetActive(true);
                enemy.transform.parent = null;
                yield return enemy;
            }
            else
            {
                break;
            }
        }
    }

    //TODO Rethink how to return enemies to pool.
    public void ReturnEnemy(RedemptionEnemyType enemyType, EnemyLite enemy)
    {
        var enemyPoolList = enemyPool[enemyType];
        var enemyIndex = enemyPoolIndex[enemyType];

        if(enemyIndex > 0)
        {
            enemy.transform.parent = poolParent;
            enemy.transform.position = Vector3.zero;
            enemyPoolIndex[enemyType]--;
            enemyPoolList[enemyIndex] = enemy;
            enemy.gameObject.SetActive(false);
        }
    }

    #region Helpers

    private void CreateRedemptionObjectPool()
    {
        if(enemyPool != null)
        {
            return;
        }

        RaiseObjectPoolBegin();

        enemyPool = new Dictionary<RedemptionEnemyType, List<EnemyLite>>();
        enemyPoolIndex = new Dictionary<RedemptionEnemyType, int>();
        foreach(var enemy in enemyList)
        {
            if(enemyPool.ContainsKey(enemy.EnemyType))
            {
                continue;
            }

            enemyPool.Add(enemy.EnemyType, new List<EnemyLite>());
            enemyPoolIndex.Add(enemy.EnemyType, 0);

            for(int count = 0; count < enemy.PoolAmount; count++)
            {
                var liteEnemy = (EnemyLite)Instantiate(enemyDictionary[enemy.EnemyType], transform.position, Quaternion.identity);
                liteEnemy.Initialize(heavyReferences.GetEnemyHeavyReference(enemy.EnemyType));

                liteEnemy.transform.parent = poolParent;
                liteEnemy.transform.position = Vector3.zero;

                enemyPool[enemy.EnemyType].Add(liteEnemy);
                liteEnemy.gameObject.SetActive(false);
            }
        }

        RaiseObjectPoolComplete();
    }

    private void InitializeEnemyDictionary()
    {
        if(enemyDictionary != null)
        {
            return;
        }
        
        enemyDictionary = new Dictionary<RedemptionEnemyType, EnemyLite>();

        foreach(var enemy in enemyList)
        {
            if(enemyDictionary.ContainsKey(enemy.EnemyType))
            {
                continue;
            }

            enemyDictionary.Add(enemy.EnemyType, enemy.EnemyLite);
        }
    }

    private void RaiseObjectPoolBegin()
    {
        var handler = ObjectPoolBegin;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    private void RaiseObjectPoolComplete()
    {
        var handler = ObjectPoolComplete;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    #endregion
}

[Serializable]
public class RedemptionEnemy
{
    public RedemptionEnemyType EnemyType;
    public EnemyLite EnemyLite;
    public int PoolAmount;
}

public enum RedemptionEnemyType
{
    BLANK,
    BLACK,
    IRON,
    LEAD,
    MAGNESIUM
}