using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : HeavyUnit
{
    #region Inspector Data

    [SerializeField]
    private int level;

    [SerializeField]
    private float baseDamage;

    [SerializeField]
    private float damageMultiplier;

    [SerializeField]
    private int cost;

    #endregion

    public int Level { get { return level; } }
    public float BaseDamage { get { return baseDamage; } }
    public float DamageMultiplier { get { return damageMultiplier; } } 
    public int Cost { get { return cost; } }
}
