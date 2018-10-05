using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputState : MonoBehaviour
{
    public InputState NextState;
    public abstract void EnterInputState(InputStateChangeArgs args = null);
    public abstract void UpdateInputState(Vector2 position);
    public virtual void ExitState(InputStateChangeArgs args = null)
    {
        PostStateChange(args);
    }

    public event EventHandler<InputStateChangeArgs> StateChange;

    protected virtual void PostStateChange(InputStateChangeArgs args)
    {
        var handler = StateChange;
        if(handler != null)
        {
            handler(this, args);
        }
    }
}
