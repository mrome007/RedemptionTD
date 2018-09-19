using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLite : MonoBehaviour, IInitializable
{
    #region Events

    public event EventHandler<EnemyReturnEventArgs> EnemyReturned;

    #endregion
    
    [SerializeField]
    private Enemy enemy;

    public Enemy HeavyEnemyReference { get { return enemy; } }

    public int Index { get; set; }

    private EnemyReturnEventArgs returnArgs;

    #region Override IInitializable

    /// <summary>
    /// Initialize the enemy's Heavy Reference.
    /// </summary>
    /// <param name="obj">Object.</param>
    public void Initialize(object obj)
    {
        enemy = obj as Enemy;

        if(enemy == null)
        {
            return;
        }

        Index = 0;
        if(returnArgs == null)
        {
            returnArgs = new EnemyReturnEventArgs(Index);
        }
    }

    public void ReturnEnemy()
    {
        HeavyEnemyReference.EnemyPool.ReturnEnemy(HeavyEnemyReference.EnemyType, this);
        RaiseReturnEnemy();
    }

    private void RaiseReturnEnemy()
    {
        returnArgs.SpawnIndex = Index;
        
        var handler = EnemyReturned;
        if(handler != null)
        {
            handler(this, returnArgs);
        }
    }

    #endregion
}
