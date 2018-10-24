using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToOrFromPoolEventArgs : EventArgs
{
    public int SpawnIndex { get; set; }
    public bool Dead { get; set; }
    public Vector3 LastPosition { get; set; }

    public ToOrFromPoolEventArgs(int index, Vector3 lastPos, bool dead = false)
    {
        SpawnIndex = index;
        Dead = dead;
        LastPosition = lastPos;
    }
}
