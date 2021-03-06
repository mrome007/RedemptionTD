﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLite : LiteUnit
{   
    private Enemy enemy;
    private EnemyMovement enemyMovement;

    //TEMPORARY just so I can see it in the editor.
    [SerializeField]
    private float currentHealth;

    #region Override

    public override HeavyUnit HeavyReference { get { return enemy; } } 

    /// <summary>
    /// Initialize the enemy's Heavy Reference.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override void Initialize(object obj)
    {
        base.Initialize(obj);

        enemy = obj as Enemy;

        if(enemy == null)
        {
            Debug.LogError("No Enemy Reference");
        }
    }

    public override void SpawnObject(int index, Vector3 position, UnitMode mode)
    {
        base.SpawnObject(index, position, mode);
        enemyMovement.Speed = enemy.Speed;
        enemyMovement.MoveEnded += HandleMoveEnded;
        currentHealth = enemy.Health;
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

    private void EnemyCollision(GameObject col)
    {
        var weaponBehavior = col.GetComponentInParent<WeaponBehavior>();
        if(weaponBehavior != null)
        {
            var damage = weaponBehavior.DamageEnemy(enemy.Color);
            currentHealth -= damage;
            if(currentHealth <= 0f)
            {
                PoolArgs.Dead = true;
                PoolArgs.LastPosition = transform.position;
                ReturnObject();
                return;
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        EnemyCollision(col.gameObject);

        var colorBase = col.GetComponentInParent<RedemptionBase>();
        if(colorBase != null)
        {
            colorBase.DamageBase(enemy.Color, enemy.DamageToBases);
            PoolArgs.Dead = false;
            ReturnObject();
            return;
        }
    }


    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        EnemyCollision(col.gameObject);
    }
}
