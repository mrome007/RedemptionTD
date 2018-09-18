using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLite : MonoBehaviour, IInitializable
{
    #region Events

    public event EventHandler EnemyReturned;

    #endregion
    
    [SerializeField]
    private Enemy enemy;

    public Enemy HeavyEnemyReference { get { return enemy; } }

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
    }

    public void ReturnEnemy()
    {
        HeavyEnemyReference.EnemyPool.ReturnEnemy(HeavyEnemyReference.EnemyType, this);
        RaiseReturnEnemy();
    }

    private void RaiseReturnEnemy()
    {
        var handler = EnemyReturned;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    #endregion
}
