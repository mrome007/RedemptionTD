using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWeaponState : WeaponState
{
    [SerializeField]
    private WeaponCycler weaponCycler;

    public override void UpdateWeapon()
    {
        
    }

    public override void EnterWeaponState(object obj = null)
    {
        weaponCycler.ShowCurrentWeapon();
    }
}
