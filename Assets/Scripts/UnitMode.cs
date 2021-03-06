﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UnitMode
{
    public WeaponMode WeaponMode;
    public EnemyMode EnemyMode;
    public ResourceMode ResourceMode;

    public static UnitMode CreateUnitMode(WeaponMode weapon = WeaponMode.NONE, 
                                          EnemyMode enemy = EnemyMode.NONE, 
                                          ResourceMode resource = ResourceMode.NONE)
    {
        var unitMode = new UnitMode();
        unitMode.WeaponMode = weapon;
        unitMode.EnemyMode = enemy;
        unitMode.ResourceMode = resource;

        return unitMode;
    }
}

public enum WeaponMode
{
    WAVE = 0,
    BURST = 1,
    GATHER = 2,
    NONE = 3
}

public enum EnemyMode
{
    NONE
}

public enum ResourceMode
{
    NONE
}