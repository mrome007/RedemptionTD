using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLite : LiteUnit
{
    #region Inspector Data

    [SerializeField]
    private Resource resource;

    #endregion

    #region Overrides

    public override HeavyUnit HeavyReference { get { return resource; } }

    public override void Initialize(object obj)
    {
        resource = obj as Resource;

        if(resource == null)
        {
            return;
        }

        Index = 0;

        if(PoolArgs == null)
        {
            PoolArgs = new ToOrFromPoolEventArgs(Index);
        }
    }

    #endregion
}
