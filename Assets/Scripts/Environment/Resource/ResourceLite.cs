using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLite : LiteUnit
{
    #region Inspector Data

    [SerializeField]
    private Resource resource;

    #endregion

    private int currentResourceCount;

    #region Overrides

    public override HeavyUnit HeavyReference { get { return resource; } }

    public override void Initialize(object obj, RedemptionTDObjectPool pool)
    {
        base.Initialize(obj, pool);

        Index = 0;

        if(PoolArgs == null)
        {
            PoolArgs = new ToOrFromPoolEventArgs(Index);
        }
    }

    protected override void ReturnObject()
    {
        gameObject.SetActive(false);
        RaiseOnReturn();
    }

    #endregion

    #region Monobehavior

    protected virtual void Start()
    {
        currentResourceCount = resource.TotalResource;
        Initialize(null, null);
    }

    #endregion

    public void GetResource()
    {
        var miss = Random.Range(0, 100);
        if(miss < resource.BustRate)
        {
            return;
        }
        
        if(currentResourceCount > 0)
        {
            currentResourceCount -= resource.GivenResource;
            ResourcesOverseer.IncreaseResourceEvent(resource.GivenResource);
        }
        else
        {
            ReturnObject();
        }
    }

    public float GetTimeToGather()
    {
        return resource.TimeToGiveResource;
    }
}
