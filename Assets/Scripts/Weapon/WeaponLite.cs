using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLite : LiteUnit
{
    #region Inspector Data

    [SerializeField]
    private Weapon weapon;

    #endregion

    #region Overrides

    public override HeavyUnit HeavyReference { get { return weapon; } } 

    /// <summary>
    /// Initialize the weapon's Heavy Reference.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override void Initialize(object obj)
    {
        weapon = obj as Weapon;

        if(weapon == null)
        {
            return;
        }

        Index = 0;

        if(ReturnArgs == null)
        {
            ReturnArgs = new ReturnToPoolEventArgs(Index);
        }
    }

    #endregion
}
