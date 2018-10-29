using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour 
{
    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private WeaponMode weaponMode;

    public event EventHandler<SpawnWeaponInputArgs> SpawnButtonClicked;

    public void OnButtonClicked()
    {
        PostSpawnButtonClicked();
    }

    private void PostSpawnButtonClicked()
    {
        var handler = SpawnButtonClicked;
        if(handler != null)
        {
            handler(this, new SpawnWeaponInputArgs(weaponMode, weapon.Type, weapon.Cost));
        }
    }
}
