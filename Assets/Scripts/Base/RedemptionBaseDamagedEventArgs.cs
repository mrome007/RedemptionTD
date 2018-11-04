using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionBaseDamagedEventArgs : EventArgs
{
    public float Damage { get; set; }

    public RedemptionBaseDamagedEventArgs(float damage)
    {
        Damage = damage;
    }
}
