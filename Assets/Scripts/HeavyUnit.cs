using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyUnit : MonoBehaviour 
{
    #region Inspector Data

    [SerializeField]
    private RedemptionTDObjectPool pool;

    [SerializeField]
    private RedemptionTDType type;

    #endregion

    public RedemptionTDObjectPool Pool { get { return pool; } }
    public RedemptionTDType Type { get { return type; } }
}
