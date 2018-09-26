using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour 
{
    [SerializeField]
    private InputState normalState;

    [SerializeField]
    private InputState spawnState;

    private InputState currentState;

    private void Awake()
    {
        currentState = spawnState;
    }

    private void Update()
    {
        currentState.UpdateInputState(Input.mousePosition);
    }
}
