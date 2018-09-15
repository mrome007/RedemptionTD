using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RedemptionTDWave))]
public class RedemptionTDWaveEditor : Editor
{
    private RedemptionTDWave redemptionWave;

    private void OnEnable()
    {
        redemptionWave = target as RedemptionTDWave;
    }

    public override void OnInspectorGUI()
    {
        if(redemptionWave == null)
        {
            redemptionWave = target as RedemptionTDWave;
        }

        DrawAddSpawnButton();
    }

    private void DrawAddSpawnButton()
    {
        if(GUILayout.Button("Add Wave", EditorStyles.miniButton))
        {
            AddWave();
        }
    }

    private void AddWave()
    {

    }
}
