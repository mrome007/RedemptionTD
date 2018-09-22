﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLite : MonoBehaviour, IInitializable, IReturnable
{
    #region Override IReturnable

    public event EventHandler<ReturnToPoolEventArgs> ObjectReturned;
    public int Index { get; set; }
    public ReturnToPoolEventArgs ReturnArgs { get; private set; }

    #endregion

    #region Inspector Data
    
    [SerializeField]
    private Enemy enemy;

    public Enemy HeavyEnemyReference { get { return enemy; } }

    #endregion

    #region Override IInitializable, IReturnable

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

        if(ReturnArgs == null)
        {
            ReturnArgs = new ReturnToPoolEventArgs(Index);
        }
    }

    public void ReturnObject()
    {
        HeavyEnemyReference.EnemyPool.ReturnEnemy(HeavyEnemyReference.EnemyType, this);
        RaiseOnReturn();
    }

    public void RaiseOnReturn()
    {
        ReturnArgs.SpawnIndex = Index;
        
        var handler = ObjectReturned;
        if(handler != null)
        {
            handler(this, ReturnArgs);
        }
    }

    #endregion
}
