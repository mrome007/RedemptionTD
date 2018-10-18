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
    private GameObject gatherObject;

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
        InitializeWeaponState();
    }

    #endregion

    #region Helpers

    protected void InitializeWeaponState()
    {
        var resourceLayer = 1 << LayerMask.NameToLayer("Resource");
        var hit = Physics2D.OverlapCircle(transform.position, weapon.GatherRadius, resourceLayer);
        if(hit != null && hit.GetComponent<LiteUnit>().Color == HeavyReference.Color)
        {
            gatherState.enabled = true;
            blastState.enabled = false;
            currentWeaponState = gatherState;
            currentWeaponState.EnterWeaponState(hit.GetComponent<ResourceLite>());
            gatherObject.SetActive(true);
        }
        else
        {
            gatherState.enabled = false;
            blastState.enabled = true;
            currentWeaponState = blastState;
            currentWeaponState.EnterWeaponState();
            gatherObject.SetActive(false);
        }
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
