using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    event EventHandler<ToOrFromPoolEventArgs> ObjectReturned;
    ToOrFromPoolEventArgs PoolArgs { get; }
    int Index { get; set; }
    void RaiseOnReturn();
    void ReturnObject();
    void RaiseOnSpawn();
    void SpawnObject(int index, Vector3 position);
}
