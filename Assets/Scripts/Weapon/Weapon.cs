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

    [SerializeField]
    private float gatherRadius;

    [SerializeField]
    private Sprite weaponSprite;

    [SerializeField]
    private Sprite gatherSprite;

    #endregion

    public int Level { get { return level; } }
    public float BaseDamage { get { return baseDamage; } }
    public float DamageMultiplier { get { return damageMultiplier; } } 
    public int Cost { get { return cost; } }
    public float GatherRadius { get { return gatherRadius; } }
    public Sprite WeaponSprite { get { return weaponSprite; } }
    public Sprite GatherSprite { get { return gatherSprite; } }
}
