using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPoolEventArgs : EventArgs
{
    public int SpawnIndex { get; set; }

    public ReturnToPoolEventArgs(int index)
    {
        SpawnIndex = index;
    }
}
