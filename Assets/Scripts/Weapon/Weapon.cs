using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
    #region Inspector Data

    [SerializeField]
    private RedemptionTDObjectPool weaponPool;

    [SerializeField]
    private int level;

    [SerializeField]
    private RedemptionTDType weaponType;

    #endregion

    public RedemptionTDObjectPool WeaponPool { get { return weaponPool; } }
    public RedemptionTDType WeaponType { get { return weaponType; } }
    public int Level { get { return level; } }
}
