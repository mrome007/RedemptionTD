using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : HeavyUnit
{
    #region Inspector Data

    [SerializeField]
    private float baseDamage;

    [SerializeField]
    private float damageMultiplier;

    [SerializeField]
    private int cost;

    [SerializeField]
    private float gatherRadius;

    [SerializeField]
    private float activeTime;

    [SerializeField]
    private float weaponSpeed;

    #endregion

    public float BaseDamage { get { return baseDamage; } }
    public float DamageMultiplier { get { return damageMultiplier; } } 
    public int Cost { get { return cost; } }
    public float GatherRadius { get { return gatherRadius; } }
    public float ActiveTime { get { return activeTime; } }
    public float WeaponSpeed { get { return weaponSpeed; } }
}
