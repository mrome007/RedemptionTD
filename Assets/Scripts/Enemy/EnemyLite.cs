using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLite : LiteUnit
{
    #region Inspector Data
    
    [SerializeField]
    private Enemy enemy;

    #endregion

    #region Override

    public override HeavyUnit HeavyReference { get { return enemy; } } 

    /// <summary>
    /// Initialize the enemy's Heavy Reference.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override void Initialize(object obj)
    {
        enemy = obj as Enemy;

        if(enemy == null)
        {
            return;
        }

        Index = 0;

        if(PoolArgs == null)
        {
            PoolArgs = new ToOrFromPoolEventArgs(Index);
        }
    }

    #endregion
}
