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
    private SpriteRenderer okSpawn;

    [SerializeField]
    private SpriteRenderer notOkSpawn;

    [SerializeField]
    private List<SpawnObject> spawnObjects;
    
    [Serializable]
    private class SpawnObject
    {
        public RedemptionTDType SpawnType;
        public Sprite SpawnSprite;
    }

    private RedemptionTDType currentType;
    private Dictionary<RedemptionTDType, Sprite> spawnObjectsDictionary;
    private int spawnIntLayer;
    private int blockIntLayer;
    private Vector2 spawnPosition;

    private void Awake()
    {
        spawnIntLayer = LayerMask.NameToLayer("Spawn");
        blockIntLayer = LayerMask.NameToLayer("Out");
        InitializeSpawnObjectsDictionary();
    }
    
    public override void EnterInputState(RedemptionTDType type = RedemptionTDType.BLANK)
    {
        currentType = type;
        RegisterSpawnButtonsClick();
        //SetOkSpawnSprite();
    }
    
    public override void UpdateInputState(Vector2 position)
    {
        var screenPoint = Camera.main.ScreenToWorldPoint(position);
        spawnPosition = screenPoint;
        transform.position = spawnPosition;

        var hit = Physics2D.Raycast(screenPoint, Vector2.zero, 0);

        if(hit.collider.gameObject.layer == blockIntLayer)
        {
            ShowOkSpawn(false);
            return;
        }

        var okToSpawn = hit.collider.gameObject.layer == spawnIntLayer;
        ToggleOkSpawn(okToSpawn);

        if(Input.GetMouseButtonDown(0) && okToSpawn)
        {
            SpawnWeapon(spawnPosition);
        }

        if(Input.GetMouseButtonDown(1))
        {
            ShowOkSpawn(false);
            ExitState();
        }
    }

    public override void ExitState(RedemptionTDType type = RedemptionTDType.BLANK)
    {
        UnRegisterSpawnButtonsClick();
        base.ExitState(type);
    }

    public void SpawnWeapon(Vector2 position)
    {
        var weapons = objectPool.GetUnits(currentType, 1);
        foreach(var weapon in weapons)
        {
            weapon.transform.position = position;
        }
    }

    private void ToggleOkSpawn(bool ok)
    {
        okSpawn.gameObject.SetActive(ok);
        notOkSpawn.gameObject.SetActive(!ok);
    }

    private void ShowOkSpawn(bool show)
    {
        okSpawn.gameObject.SetActive(show);
        notOkSpawn.gameObject.SetActive(show);
    }

    private void SetOkSpawnSprite()
    {
        var ok = spawnObjectsDictionary[currentType];
        okSpawn.sprite = ok;
        notOkSpawn.sprite = ok;
    }

    private void InitializeSpawnObjectsDictionary()
    {
        if(spawnObjectsDictionary != null)
        {
            return;
        }
        spawnObjectsDictionary = new Dictionary<RedemptionTDType, Sprite>();
        foreach(var spawn in spawnObjects)
        {
            if(spawnObjectsDictionary.ContainsKey(spawn.SpawnType))
            {
                continue;
            }
            spawnObjectsDictionary.Add(spawn.SpawnType, spawn.SpawnSprite);
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

    private void HandleSpawnButtonClicked(object sender, RedemptionTDTypeEventArgs e)
    {
        currentType = e.Type;
        //SetOkSpawnSprite();
    }
}
