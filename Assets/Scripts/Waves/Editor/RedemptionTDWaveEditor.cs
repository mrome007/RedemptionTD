using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(RedemptionTDWave))]
public class RedemptionTDWaveEditor : Editor
{
    private RedemptionTDWave redemptionWave;

    private void OnEnable()
    {
        redemptionWave = target as RedemptionTDWave;
        InitializeSpawnInfos();
    }

    public override void OnInspectorGUI()
    {
        if(redemptionWave == null)
        {
            redemptionWave = target as RedemptionTDWave;
        }

        redemptionWave.StaggeredSpawning = EditorGUILayout.Toggle("Staggered Spawn: ", redemptionWave.StaggeredSpawning);
        redemptionWave.EnemyIndicatorPrefab = EditorGUILayout.ObjectField("Indicator Object: ", redemptionWave.EnemyIndicatorPrefab, typeof(GameObject), true) as GameObject;

        DrawnSpawnInfos();
        DrawAddSpawnButton();

        if(GUI.changed)
        {
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }

    private void DrawAddSpawnButton()
    {
        if(GUILayout.Button("Add Wave"))
        {
            AddWave();
            GUI.changed = true;
        }
    }

    private void DrawnSpawnInfos()
    {
        if(redemptionWave.SpawnInformation == null || redemptionWave.SpawnInformation.Count == 0)
        {
            return;
        }

        EditorGUILayout.LabelField("Spawn Information:");

        var count = 0;
        foreach(var spawn in redemptionWave.SpawnInformation)
        {
            GUILayout.BeginVertical();
            EditorGUI.indentLevel++;

            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Spawn " + count.ToString());
            spawn.gameObject.name = "Spawn " + count.ToString();
            if(!Application.isPlaying)
            {
                if(GUILayout.Button("Remove"))
                {
                    Object.DestroyImmediate(spawn.gameObject);
                    InitializeSpawnInfos(true);
                    GUI.changed = true;
                    return;
                }
            }

            GUILayout.EndHorizontal();

            EditorGUI.indentLevel++;

            spawn.StartSpawnDelay = EditorGUILayout.FloatField("Start Spawn Delay: ", spawn.StartSpawnDelay);
            spawn.StopSpawnDelay = EditorGUILayout.FloatField("Stop Spawn Delay: ", spawn.StopSpawnDelay);
            spawn.TimeBetweenSpawns = EditorGUILayout.FloatField("Time BetWeen Spawns: ", spawn.TimeBetweenSpawns);
            spawn.NumberToSpawn = EditorGUILayout.IntField("Number To Spawn: ", spawn.NumberToSpawn);

            spawn.EnemyType = (RedemptionTDType)EditorGUILayout.EnumPopup("Enemy Type: ", spawn.EnemyType);
            spawn.IsBoss = EditorGUILayout.Toggle("Is Boss", spawn.IsBoss);

            spawn.SpawnPosition = EditorGUILayout.ObjectField("Position: ", spawn.SpawnPosition, typeof(Waypoint), true) as Waypoint;

            EditorGUI.indentLevel--;

            EditorGUI.indentLevel--;
            GUILayout.EndVertical();
            
            count++;
        }
    }

    private void AddWave()
    {
        var count = redemptionWave.SpawnInformation == null ? 0 : redemptionWave.SpawnInformation.Count;
        var spawnObject = new GameObject("Spawn " + count.ToString());
        spawnObject.transform.parent = redemptionWave.transform;
        spawnObject.AddComponent<RedemptionTDSpawnInfo>();
        redemptionWave.SpawnInformation.Add(spawnObject.GetComponent<RedemptionTDSpawnInfo>());
    }

    private void InitializeSpawnInfos(bool clear = false)
    {
        if(redemptionWave.SpawnInformation == null)
        {
            redemptionWave.SpawnInformation = new List<RedemptionTDSpawnInfo>();
            return;

        }

        if(!clear)
        {
            return;
        }

        redemptionWave.SpawnInformation.Clear();
        var children = redemptionWave.GetComponentsInChildren<RedemptionTDSpawnInfo>();
        foreach(var child in children)
        {
            redemptionWave.SpawnInformation.Add(child);
        }
    }
}
