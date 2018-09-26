using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionTDTypeEventArgs : EventArgs
{
    public RedemptionTDType Type { get; set; }

    public RedemptionTDTypeEventArgs(RedemptionTDType type)
    {
        Type = type;
    }
}
