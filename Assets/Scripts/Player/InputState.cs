using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputState : MonoBehaviour
{
    public abstract void EnterInputState(RedemptionTDType type = RedemptionTDType.BLANK);
    public abstract void UpdateInputState(Vector2 position);
}
