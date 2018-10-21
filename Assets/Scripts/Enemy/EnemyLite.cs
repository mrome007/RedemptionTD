using System;
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
    public override void Initialize(object obj, RedemptionTDObjectPool pool)
    {
        base.Initialize(obj, pool);

        enemy = obj as Enemy;

        if(enemy == null)
        {
            Debug.LogError("No Enemy Reference");
        }
    }

    public override void SpawnObject(int index, Vector3 position)
    {
        base.SpawnObject(index, position);
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

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        var weaponBehavior = col.GetComponent<WeaponBehavior>();
        if(weaponBehavior != null)
        {
            var damage = weaponBehavior.DamageEnemy(enemy.Color);
            currentHealth -= damage;
            if(currentHealth <= 0f)
            {
                SpawnResourceDrops();
                ReturnObject();
            }
        }
    }

    public void SpawnResourceDrops()
    {
        var numToSpawn = UnityEngine.Random.Range(0, enemy.MaxResourceDrops);
        var units = objectPool.GetUnits(GetResourceDropType(), numToSpawn);
        var count = 0;
        foreach(var resourceDrop in units)
        {
            var position = new Vector3(transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f), 
                                       transform.position.y + UnityEngine.Random.Range(-0.1f, 0.1f), 
                                       -0.1f);
            resourceDrop.SpawnObject(count, position);
            count++;
        }
    }

    private RedemptionTDType GetResourceDropType()
    {
        var result = RedemptionTDType.BLACK_RESOURCE_DROP;
        switch(enemy.Color)
        {
            case RedemptionTDColor.BLACK:
                result = RedemptionTDType.BLACK_RESOURCE_DROP;
                break;

            case RedemptionTDColor.IRON:
                result = RedemptionTDType.IRON_RESOURCE_DROP;
                break;

            case RedemptionTDColor.LEAD:
                result = RedemptionTDType.LEAD_RESOURCE_DROP;
                break;

            case RedemptionTDColor.MAGNESIUM:
                result = RedemptionTDType.MAGNESIUM_RESOURCE_DROP;
                break;
            default:
                break;
        }

        return result;
    }
}
