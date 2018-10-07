using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLite : LiteUnit
{   
    private Enemy enemy;
    private EnemyMovement enemyMovement;

    #region Override

    protected override HeavyUnit HeavyReference { get { return enemy; } } 

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

    public override void SpawnObject(int index, Vector3 position)
    {
        base.SpawnObject(index, position);
        enemyMovement.Speed = enemy.Speed;
        enemyMovement.MoveEnded += HandleMoveEnded;
    }

    protected override void ReturnObject()
    {
        enemyMovement.MoveEnded -= HandleMoveEnded;
        base.ReturnObject();
    }

    #endregion

    protected virtual void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void HandleMoveEnded(object sender, EventArgs e)
    {
        enemyMovement.MoveEnded -= HandleMoveEnded;
        ReturnObject();
    }
}
