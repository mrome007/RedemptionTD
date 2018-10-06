using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToOrFromPoolEventArgs : EventArgs
{
    public int SpawnIndex { get; set; }

    public ToOrFromPoolEventArgs(int index)
    {
        SpawnIndex = index;
    }
}
