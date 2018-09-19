using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturnEventArgs : EventArgs
{
    public int SpawnIndex { get; set; }

    public EnemyReturnEventArgs(int index)
    {
        SpawnIndex = index;
    }
}
