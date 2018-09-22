﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LiteUnit : MonoBehaviour, IInitializable, IReturnable
{
    #region Override IReturnable

    public event EventHandler<ReturnToPoolEventArgs> ObjectReturned;
    public ReturnToPoolEventArgs ReturnArgs { get; protected set; }
    public int Index { get; set; }

    public virtual void RaiseOnReturn()
    {
        ReturnArgs.SpawnIndex = Index;

        var handler = ObjectReturned;
        if(handler != null)
        {
            handler(this, ReturnArgs);
        }
    }

    public virtual void ReturnObject()
    {
        //return lite object here.
        RaiseOnReturn();
    }

    #endregion

    #region Override IInitializable

    public abstract void Initialize(object obj);

    #endregion
}
