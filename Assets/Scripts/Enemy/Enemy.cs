using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    #region Inspector Data

    [SerializeField]
    private RedemptionTDObjectPool objectPool;

    [SerializeField]
    private RedemptionTDType enemyType;

    [SerializeField]
    private float speed;

    #endregion

    public RedemptionTDObjectPool EnemyPool { get { return objectPool; } }
    public RedemptionTDType EnemyType { get { return enemyType; } }
    public float Speed { get { return speed; } }
}
