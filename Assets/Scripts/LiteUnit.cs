using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LiteUnit : MonoBehaviour, IInitializable
{
    protected virtual HeavyUnit HeavyReference { get; }
    
    #region Override IReturnable

    public event EventHandler<ToOrFromPoolEventArgs> ObjectReturned;
    public event EventHandler<ToOrFromPoolEventArgs> ObjectSpawned;
    public RedemptionTDColor Color { get { return HeavyReference.Color; } }
    protected ToOrFromPoolEventArgs PoolArgs { get; set; }
    protected int Index { get; set; }

    public virtual void RaiseOnReturn()
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
        HeavyReference.Pool.ReturnUnit(HeavyReference.Type, this);
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

    public virtual void SpawnObject(int index, Vector3 position)
    {
        Index = index;
        transform.position = position;
        RaiseOnSpawn();
    }

    #endregion

    #region Override IInitializable

    public abstract void Initialize(object obj);

    #endregion
}
