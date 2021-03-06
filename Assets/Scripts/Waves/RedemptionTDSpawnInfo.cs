﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionTDSpawnInfo : MonoBehaviour 
{
    [HideInInspector]
    public float StartSpawnDelay;

    [HideInInspector]
    public float StopSpawnDelay;

    [HideInInspector]
    public float TimeBetweenSpawns;

    [HideInInspector]
    public int NumberToSpawn;

    [HideInInspector]
    public RedemptionTDType EnemyType;

    [HideInInspector]
    public bool IsBoss;

    [HideInInspector]
    public Waypoint SpawnPosition;
}
