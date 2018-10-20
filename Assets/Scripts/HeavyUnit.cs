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

    #endregion

    public RedemptionTDType Type { get { return type; } }
    public RedemptionTDColor Color { get { return color; } }
}
