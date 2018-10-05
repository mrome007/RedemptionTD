using System;
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
        currentState = normalState;
        currentState.StateChange += HandleStateChange;
    }

    private void Start()
    {
        currentState.EnterInputState();
    }

    private void OnDestroy()
    {
        normalState.StateChange -= HandleStateChange;
        spawnState.StateChange -= HandleStateChange;
    }

    private void HandleStateChange(object sender, InputStateChangeArgs e)
    {
        currentState.StateChange -= HandleStateChange;
        currentState = currentState.NextState;
        currentState.EnterInputState(e);
        currentState.StateChange += HandleStateChange;
    }

    private void Update()
    {
        currentState.UpdateInputState(Input.mousePosition);
    }
}
