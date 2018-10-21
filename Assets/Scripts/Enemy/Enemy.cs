using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HeavyUnit
{
    #region Inspector Data

    [SerializeField]
    private float speed;

    [SerializeField]
    private float health;

    [SerializeField]
    private int maxResourceDrops;

    #endregion

    public float Speed { get { return speed; } }
    public float Health { get { return health; } }
    public int MaxResourceDrops { get { return maxResourceDrops; } }
}
