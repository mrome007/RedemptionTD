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
        if(Input.GetMouseButtonDown(0))
        {
            ClickedResourceDrop(position);
        }
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

    private void ClickedResourceDrop(Vector2 mousePos)
    {
        var screenPoint = Camera.main.ScreenToWorldPoint(mousePos);
        var hit = Physics2D.Raycast(screenPoint, Vector2.zero, 0);

        var resourceDrop = hit.collider.gameObject.GetComponent<ResourceDropLite>();
        if(resourceDrop != null)
        {
            resourceDrop.GiveDroppedResource();
        }
    }
}
