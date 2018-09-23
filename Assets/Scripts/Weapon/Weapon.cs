using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : HeavyUnit
{
    #region Inspector Data

    [SerializeField]
    private int level;

    #endregion

    public int Level { get { return level; } }
}
