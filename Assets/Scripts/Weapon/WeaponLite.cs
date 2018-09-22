using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLite : MonoBehaviour, IInitializable, IReturnable
{
    #region Override IReturnable

    public event EventHandler<ReturnToPoolEventArgs> ObjectReturned;
    public int Index { get; set; }
    public ReturnToPoolEventArgs ReturnArgs { get; private set; }

    #endregion

    #region Inspector Data

    [SerializeField]
    private Weapon weapon;

    public Weapon HeavyWeaponReference { get { return weapon; } }

    #endregion

    #region Override IInitializable, IReturnable

    /// <summary>
    /// Initialize the weapon's Heavy Reference.
    /// </summary>
    /// <param name="obj">Object.</param>
    public void Initialize(object obj)
    {
        weapon = obj as Weapon;

        if(weapon == null)
        {
            return;
        }

        Index = 0;

        if(ReturnArgs == null)
        {
            ReturnArgs = new ReturnToPoolEventArgs(Index);
        }
    }

    public void ReturnObject()
    {
        HeavyWeaponReference.WeaponPool.ReturnWeapon(HeavyWeaponReference.WeaponType, this);
        RaiseOnReturn();
    }

    public void RaiseOnReturn()
    {
        ReturnArgs.SpawnIndex = Index;

        var handler = ObjectReturned;
        if(handler != null)
        {
            handler(this, ReturnArgs);
        }
    }

    #endregion
}
