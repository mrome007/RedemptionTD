using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeaponInputArgs : InputStateChangeArgs
{
    public Weapon Weapon { get; private set; }

    public SpawnWeaponInputArgs(Weapon weapon)
    {
        Weapon = weapon;
    }
}
