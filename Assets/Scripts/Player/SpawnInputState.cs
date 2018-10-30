using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInputState : InputState
{
    [SerializeField]
    private RedemptionTDObjectPool objectPool;
    
    [SerializeField]
    private List<SpawnButton> spawnButtons;

    [SerializeField]
    private SpawnCursor spawnCursor;

    private SpawnWeaponInputArgs currentWeapon;
    private int spawnIntLayer;
    private int blockIntLayer;
    private Vector2 spawnPosition;

    private void Awake()
    {
        spawnIntLayer = LayerMask.NameToLayer("Spawn");
        blockIntLayer = LayerMask.NameToLayer("Out");
    }
    
    public override void EnterInputState(InputStateChangeArgs args = null)
    {
        currentWeapon = args as SpawnWeaponInputArgs;
        RegisterSpawnButtonsClick();
    }
    
    public override void UpdateInputState(Vector2 position)
    {        
        var screenPoint = Camera.main.ScreenToWorldPoint(position);
        spawnPosition = screenPoint;
        transform.position = spawnPosition;

        var hit = Physics2D.Raycast(screenPoint, Vector2.zero, 0);

        if(hit.collider.gameObject.layer == blockIntLayer)
        {
            spawnCursor.ShowOkSpawn(false);
            return;
        }

        var weapon = hit.collider.GetComponentInParent<WeaponLite>();
        if(weapon != null)
        {
            transform.position = hit.collider.transform.position;
            ShowUpgrade(weapon);


            return;
        }

        var okToSpawn = hit.collider.gameObject.layer == spawnIntLayer;
        spawnCursor.ToggleOkSpawn(okToSpawn);

        if(Input.GetMouseButtonDown(0) && okToSpawn)
        {
            SpawnWeapon(spawnPosition);
            return;
        }

        if(Input.GetMouseButtonDown(1))
        {
            spawnCursor.ShowOkSpawn(false);
            ExitState();
        }
    }

    public override void ExitState(InputStateChangeArgs args = null)
    {
        UnRegisterSpawnButtonsClick();
        base.ExitState(args);
    }

    private void ShowUpgrade(WeaponLite weapon)
    {
        var heavy = objectPool.GetHeavyUnit(weapon.HeavyReference.Type, weapon.HeavyReference.Level + 1) as Weapon;
        var cost = heavy.Cost;
        var okToUpgrade = weapon.CanUpgrade() && ResourcesOverseer.CanChangeResourceCount(false, cost);
        var resize = 2.5f;
        spawnCursor.ToggleOkSpawn(okToUpgrade, resize);

        if(Input.GetMouseButtonDown(0) && okToUpgrade)
        {
            ResourcesOverseer.DecreaseResourceEvent(cost);
            weapon.Upgrade(objectPool.GetHeavyUnit(weapon.HeavyReference.Type, weapon.HeavyReference.Level + 1));
            return;
        }

        if(Input.GetMouseButtonDown(1))
        {
            //Sell
            ExitState();
        }
    }

    private void SpawnWeapon(Vector2 position)
    {
        if(!ResourcesOverseer.DecreaseResourceEvent(currentWeapon.WeaponCost))
        {
            return;
        }

        var weapons = objectPool.GetUnits(currentWeapon.WeaponType, 1);
        var weaponMode = UnitMode.CreateUnitMode(currentWeapon.WeaponMode);
        foreach(var weapon in weapons)
        {
            weapon.SpawnObject(0, position, weaponMode);
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
        currentWeapon = e;
    }
}
