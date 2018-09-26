using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputState : MonoBehaviour
{
    public InputState NextState;
    public abstract void EnterInputState(RedemptionTDType type = RedemptionTDType.BLANK);
    public abstract void UpdateInputState(Vector2 position);
    public virtual void ExitState(RedemptionTDType type = RedemptionTDType.BLANK)
    {
        PostStateChange(type);
    }

    public event EventHandler<RedemptionTDTypeEventArgs> StateChange;

    protected virtual void PostStateChange(RedemptionTDType type)
    {
        var handler = StateChange;
        if(handler != null)
        {
            handler(this, type == RedemptionTDType.BLANK ? null : new RedemptionTDTypeEventArgs(type));
        }
    }
}
