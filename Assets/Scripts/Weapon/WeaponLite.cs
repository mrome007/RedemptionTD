using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLite : LiteUnit
{
    #region Inspector Data

    [SerializeField]
    private WeaponCycler weaponCycler;

    [SerializeField]
    private WeaponResizer weaponResizer;

    [SerializeField]
    private WeaponStateCycler weaponStateCycler;

    #endregion

    private Weapon weapon;

    #region Overrides

    public override HeavyUnit HeavyReference { get { return weapon; } } 

    /// <summary>
    /// Initialize the weapon's Heavy Reference.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override void Initialize(object obj)
    {
        base.Initialize(obj);

        weapon = obj as Weapon;

        weaponResizer.ResizeWeapon(weapon.Level);

        if(weapon == null)
        {
            Debug.LogError("No Weapon Reference");
        }
    }

    public override void SpawnObject(int index, Vector3 position, UnitMode mode)
    {
        base.SpawnObject(index, position, mode);
        InitializeWeaponState(mode.WeaponMode);
    }

    #endregion

    #region Helpers

    public void Sell()
    {
        ReturnObject();
    }

    private void InitializeWeaponState(WeaponMode mode)
    {
        var state = weaponStateCycler.InitializeWeaponState(mode, weapon.GatherRadius, weapon.Color);
        weaponCycler.ShowCurrentWeapon(mode, state == WeaponMode.GATHER);
    }

    #endregion

    #region Monobehavior

    protected virtual void Update()
    {
        weaponStateCycler.CurrentWeaponState.UpdateWeapon();
    }

    #endregion
}
