using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionBaseDestroyedEventArgs : EventArgs
{
    public int Index { get; private set; }

    public RedemptionBaseDestroyedEventArgs(int index)
    {
        Index = index;
    }
}
