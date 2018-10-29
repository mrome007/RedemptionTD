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

    [SerializeField]
    private NullWeaponState nullState;

    [SerializeField]
    private WeaponCycler weaponCycler;

    [SerializeField]
    private GameObject weaponObject;

    [SerializeField]
    private GameObject gatherObject;

    #endregion

    private Weapon weapon;
    private WeaponState currentWeaponState;
    private const float upgradeSizeIncr = 0.15f;

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

        ResizeWeapon();

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

    private void InitializeWeaponState(WeaponMode mode)
    {
        if(mode == WeaponMode.GATHER)
        {
            var resourceLayer = 1 << LayerMask.NameToLayer("Resource");
            var hit = Physics2D.OverlapCircle(transform.position, weapon.GatherRadius, resourceLayer);

            if(hit != null && hit.GetComponent<LiteUnit>().HeavyReference.Color == HeavyReference.Color)
            {
                gatherState.enabled = true;
                blastState.enabled = false;
                currentWeaponState = gatherState;
                currentWeaponState.EnterWeaponState(hit.GetComponent<ResourceLite>());
            }

            gatherObject.SetActive(true);
            weaponObject.SetActive(false);
        }
        else
        {
            gatherState.enabled = false;
            blastState.enabled = true;
            currentWeaponState = blastState;
            currentWeaponState.EnterWeaponState();
            gatherObject.SetActive(false);
            weaponObject.SetActive(true);
        }

        weaponCycler.ShowCurrentWeapon(mode);
    }

    private void ResizeWeapon()
    {
        var scaleWeapon = weaponObject.transform.localScale;
        var scaleGather = gatherObject.transform.localScale;

        var newScale = 1f + ((weapon.Level - 1) * upgradeSizeIncr);
        scaleWeapon.x = scaleWeapon.y = scaleGather.x = scaleGather.y = newScale;
        weaponObject.transform.localScale = scaleWeapon;
        gatherObject.transform.localScale = scaleGather;
    }

    #endregion

    #region Monobehavior

    protected virtual void OnEnable()
    {
        currentWeaponState = nullState;
        currentWeaponState.EnterWeaponState();
    }

    protected virtual void Update()
    {
        currentWeaponState.UpdateWeapon();
    }

    #endregion
}
