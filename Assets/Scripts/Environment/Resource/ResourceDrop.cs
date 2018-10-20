using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDrop : HeavyUnit
{
    [SerializeField]
    private int resourceCount;

    public int ResourceCount { get { return resourceCount; } }
}
