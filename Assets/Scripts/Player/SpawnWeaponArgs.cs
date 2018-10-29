using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeaponInputArgs : InputStateChangeArgs
{
    public WeaponMode WeaponMode { get; private set; }
    public RedemptionTDType WeaponType { get; private set; }
    public int WeaponCost { get; private set;}

    public SpawnWeaponInputArgs(WeaponMode weaponMode, RedemptionTDType type, int cost)
    {
        WeaponMode = weaponMode;
        WeaponType = type;
        WeaponCost = cost;
    }
}
