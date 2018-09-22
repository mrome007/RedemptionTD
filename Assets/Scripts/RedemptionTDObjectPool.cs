using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionTDObjectPool : MonoBehaviour 
{
    #region Public Data

    public event EventHandler ObjectPoolBegin;

    public event EventHandler ObjectPoolComplete;

    #endregion
    
    #region Inspector Data

    [SerializeField]
    private List<RedemptionEnemy> enemyList;

    [SerializeField]
    private EnemyHeavyReferences enemyHeavyReferences;

    [SerializeField]
    private List<RedemptionWeapon> weaponList;

    [SerializeField]
    private WeaponHeavyReferences weaponHeavyReferences;

    [SerializeField]
    private Transform enemyPoolParent;

    [SerializeField]
    private Transform weaponPoolParent;

    #endregion

    #region Private Data

    private Dictionary<RedemptionTDType, EnemyLite> enemyDictionary;
    private Dictionary<RedemptionTDType, List<EnemyLite>> enemyPool;
    private Dictionary<RedemptionTDType, int> enemyPoolIndex;

    #endregion

    private void Awake()
    {
        InitializeEnemyDictionary();
        CreateRedemptionObjectPool();
    }

    public IEnumerable<EnemyLite> GetEnemies(RedemptionTDType enemyType, int numberOfEnemies)
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

    public void ReturnEnemy(RedemptionTDType enemyType, EnemyLite enemy)
    {
        var enemyPoolList = enemyPool[enemyType];
        var enemyIndex = enemyPoolIndex[enemyType];

        if(enemyIndex > 0)
        {
            enemy.transform.parent = enemyPoolParent;
            enemy.transform.position = Vector3.zero;
            enemyPoolIndex[enemyType]--;
            enemyPoolList[enemyIndex] = enemy;
            enemy.gameObject.SetActive(false);
        }
    }

    public void ReturnWeapon(RedemptionTDType weaponType, WeaponLite weapon)
    {

    }

    #region Helpers

    private void CreateRedemptionObjectPool()
    {
        if(enemyPool != null)
        {
            return;
        }

        RaiseObjectPoolBegin();

        enemyPool = new Dictionary<RedemptionTDType, List<EnemyLite>>();
        enemyPoolIndex = new Dictionary<RedemptionTDType, int>();
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
                liteEnemy.Initialize(enemyHeavyReferences.GetEnemyHeavyReference(enemy.EnemyType));

                liteEnemy.transform.parent = enemyPoolParent;
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
        
        enemyDictionary = new Dictionary<RedemptionTDType, EnemyLite>();

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
        Debug.Log("Object Pool Begin");

        var handler = ObjectPoolBegin;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    private void RaiseObjectPoolComplete()
    {
        Debug.Log("Object Pool Complete");

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
    public RedemptionTDType EnemyType;
    public EnemyLite EnemyLite;
    public int PoolAmount;
}

[Serializable]
public class RedemptionWeapon
{
    public RedemptionTDType WeaponType;
    public WeaponLite WeaponLite;
    public int PoolAmount;
}

public enum RedemptionTDType
{
    BLANK,
    BLACK,
    IRON,
    LEAD,
    MAGNESIUM
}