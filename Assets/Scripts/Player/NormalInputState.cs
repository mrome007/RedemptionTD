using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalInputState : InputState
{
    [SerializeField]
    private List<SpawnButton> spawnButtons;

    public override void EnterInputState(InputStateChangeArgs args = null)
    {
        RegisterSpawnButtonsClick();
    }
    
    public override void UpdateInputState(Vector2 position)
    {
        
    }

    private void RegisterSpawnButtonsClick()
    {
        foreach(var spawn in spawnButtons)
        {
            spawn.SpawnButtonClicked += HandleSpawnButtonClicked;
        }
    }

    private void UnRegisterSpawnButtonsClick()
    {
        foreach(var spawn in spawnButtons)
        {
            spawn.SpawnButtonClicked -= HandleSpawnButtonClicked;
        }
    }

    private void HandleSpawnButtonClicked(object sender, SpawnWeaponInputArgs e)
    {
        UnRegisterSpawnButtonsClick();
        ExitState(e);
    }
}
