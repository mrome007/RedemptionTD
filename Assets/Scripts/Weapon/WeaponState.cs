using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponState : MonoBehaviour 
{
    public abstract void UpdateWeapon();

    protected virtual void Start()
    {

    }
}
