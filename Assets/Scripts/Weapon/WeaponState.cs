using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponState : MonoBehaviour 
{
    public abstract void UpdateWeapon();
    public abstract void EnterWeaponState(object obj = null);
}
