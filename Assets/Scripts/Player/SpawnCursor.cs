using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCursor : MonoBehaviour 
{
    [Serializable]
    private class SpawnObject
    {
        [SerializeField]
        public RedemptionTDType SpawnType;

        [SerializeField]
        public Sprite SpawnSprite;
    }

    [SerializeField]
    private SpriteRenderer okSpawn;

    [SerializeField]
    private SpriteRenderer notOkSpawn;

    [SerializeField]
    private List<SpawnObject> spawnObjects;

    private Dictionary<RedemptionTDType, Sprite> spawnObjectsDictionary;

    private void Awake()
    {
        InitializeSpawnObjectsDictionary();
    }

    public void ToggleOkSpawn(bool ok)
    {
        okSpawn.gameObject.SetActive(ok);
        notOkSpawn.gameObject.SetActive(!ok);
    }

    public void ShowOkSpawn(bool show)
    {
        okSpawn.gameObject.SetActive(show);
        notOkSpawn.gameObject.SetActive(show);
    }

    private void SetOkSpawnSprite(RedemptionTDType currentType)
    {
        var ok = spawnObjectsDictionary[currentType];
        okSpawn.sprite = ok;
        notOkSpawn.sprite = ok;
    }

    public void InitializeSpawnObjectsDictionary()
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
