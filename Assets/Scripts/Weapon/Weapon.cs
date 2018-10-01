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

    #endregion

    public int Level { get { return level; } }
    public float BaseDamage { get { return baseDamage; } }
    public float DamageMultiplier { get { return damageMultiplier; } } 
}
