using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    #region Inspector Data

    [SerializeField]
    private RedemptionEnemyObjectPool objectPool;

    [SerializeField]
    private RedemptionEnemyType enemyType;

    [SerializeField]
    private float speed;

    #endregion

    public RedemptionEnemyObjectPool EnemyPool { get { return objectPool; } }
    public RedemptionEnemyType EnemyType { get { return enemyType; } }
    public float Speed { get { return speed; } }
}
