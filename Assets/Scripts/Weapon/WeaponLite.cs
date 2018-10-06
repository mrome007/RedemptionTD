using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLite : LiteUnit
{
    #region Inspector Data

    [SerializeField]
    private BlastWeaponState blastState;

    [SerializeField]
    private GatherWeaponState gatherState;

    #endregion

    private Weapon weapon;
    private WeaponState currentWeaponState;

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

        if(PoolArgs == null)
        {
            PoolArgs = new ToOrFromPoolEventArgs(Index);
        }
    }

    public override void SpawnObject(int index, Vector3 position)
    {
        base.SpawnObject(index, position);

        var resourceLayer = 1 << LayerMask.NameToLayer("Resource");
        var hit = Physics2D.OverlapCircle(position, weapon.GatherRadius, resourceLayer);
        if(hit != null)
        {
            gatherState.enabled = true;
            blastState.enabled = false;
            currentWeaponState = gatherState;
        }
        else
        {
            gatherState.enabled = false;
            blastState.enabled = true;
            currentWeaponState = blastState;
        }
    }

    #endregion
}
