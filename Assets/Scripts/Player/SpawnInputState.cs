using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInputState : InputState
{
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
    private Vector2 spawnPosition;
    private void Awake()
    {
        spawnIntLayer = LayerMask.NameToLayer("Spawn");
        InitializeSpawnObjectsDictionary();
    }
    
    public override void EnterInputState(RedemptionTDType type = RedemptionTDType.BLANK)
    {
        currentType = type;
    }
    
    public override void UpdateInputState(Vector2 position)
    {
        var screenPoint = Camera.main.ScreenToWorldPoint(position);
        spawnPosition = screenPoint;
        transform.position = spawnPosition;
        var hit = Physics2D.Raycast(screenPoint, Vector2.zero, 0);

        ToggleOkSpawn(hit.collider.gameObject.layer == spawnIntLayer);
    }

    private void ToggleOkSpawn(bool ok)
    {
        okSpawn.gameObject.SetActive(ok);
        notOkSpawn.gameObject.SetActive(!ok);
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
}
