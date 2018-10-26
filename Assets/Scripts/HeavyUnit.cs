using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyUnit : MonoBehaviour 
{
    #region Inspector Data

    [SerializeField]
    private RedemptionTDColor color;

    [SerializeField]
    private RedemptionTDType type;

    [SerializeField]
    private int level;

    [SerializeField]
    private int maxLevel;

    #endregion

    public RedemptionTDType Type { get { return type; } }
    public RedemptionTDColor Color { get { return color; } }
    public int Level { get { return level; } }
    public int MaxLevel { get { return maxLevel; } }
}
