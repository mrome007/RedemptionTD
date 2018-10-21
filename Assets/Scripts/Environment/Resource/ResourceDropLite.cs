using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDropLite : LiteUnit
{
    private ResourceDrop drop;

    #region Overrides

    public override HeavyUnit HeavyReference { get { return drop; } } 

    public override void Initialize(object obj, RedemptionTDObjectPool pool)
    {
        base.Initialize(obj, pool);

        drop = obj as ResourceDrop;

        if(drop == null)
        {
            Debug.LogError("No ResourceDrop Reference");
        }
    }

    #endregion

    public void GiveDroppedResource()
    {
        ResourcesOverseer.IncreaseResourceEvent(drop.ResourceCount);
        ReturnObject();
    }
}
