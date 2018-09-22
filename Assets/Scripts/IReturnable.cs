using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReturnable
{
    event EventHandler<ReturnToPoolEventArgs> ObjectReturned;
    ReturnToPoolEventArgs ReturnArgs { get; }
    int Index { get; set; }
    void RaiseOnReturn();
    void ReturnObject();
}
