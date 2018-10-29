using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LiteUnit : MonoBehaviour
{
    public virtual HeavyUnit HeavyReference { get; }
    
    #region Override IReturnable

    public event EventHandler<ToOrFromPoolEventArgs> ObjectReturned;
    public event EventHandler<ToOrFromPoolEventArgs> ObjectSpawned;
    protected ToOrFromPoolEventArgs PoolArgs { get; set; }
    protected int Index { get; set; }

    protected virtual void RaiseOnReturn()
    {
        PoolArgs.SpawnIndex = Index;

        var handler = ObjectReturned;
        if(handler != null)
        {
            handler(this, PoolArgs);
        }
    }

    protected virtual void ReturnObject()
    {
        RaiseOnReturn();
    }

    protected virtual void RaiseOnSpawn()
    {
        PoolArgs.SpawnIndex = Index;

        var handler = ObjectSpawned;
        if(handler != null)
        {
            handler(this, PoolArgs);
        }
    }

    public virtual void SpawnObject(int index, Vector3 position, UnitMode mode)
    {
        Index = index;
        transform.position = position;
        RaiseOnSpawn();
    }

    public virtual void Upgrade(HeavyUnit newHeavyReference)
    {
        if(!CanUpgrade())
        {
            return;
        }

        Initialize(newHeavyReference);
    }

    public virtual bool CanUpgrade()
    {
        return HeavyReference.Level < HeavyReference.MaxLevel;
    }

    #endregion

    #region Override IInitializable

    public virtual void Initialize(object obj)
    {
        if(PoolArgs == null)
        {
            PoolArgs = new ToOrFromPoolEventArgs(Index, Vector3.zero);
        }
    }

    #endregion
}
