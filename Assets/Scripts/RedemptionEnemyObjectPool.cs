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

    #endregion

    private void Awake()
    {
        InitializeEnemyDictionary();
        CreateRedemptionObjectPool();
    }

    private void CreateRedemptionObjectPool()
    {
        if(enemyPool != null)
        {
            return;
        }

        RaiseObjectPoolBegin();

        enemyPool = new Dictionary<RedemptionEnemyType, List<EnemyLite>>();
        foreach(var enemy in enemyList)
        {
            if(enemyPool.ContainsKey(enemy.EnemyType))
            {
                continue;
            }

            enemyPool.Add(enemy.EnemyType, new List<EnemyLite>());

            for(int count = 0; count < enemy.PoolAmount; count++)
            {
                var liteEnemy = (EnemyLite)Instantiate(enemyDictionary[enemy.EnemyType], transform.position, Quaternion.identity);
                liteEnemy.Initialize(heavyReferences.GetEnemyHeavyReference(enemy.EnemyType));

                liteEnemy.transform.parent = poolParent;
                liteEnemy.transform.position = Vector3.zero;

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