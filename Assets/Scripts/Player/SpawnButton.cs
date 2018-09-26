using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour 
{
    [SerializeField]
    private RedemptionTDType spawnType;

    public event EventHandler<RedemptionTDTypeEventArgs> SpawnButtonClicked;

    public void OnButtonClicked()
    {
        PostSpawnButtonClicked();
    }

    private void PostSpawnButtonClicked()
    {
        var handler = SpawnButtonClicked;
        if(handler != null)
        {
            handler(this, new RedemptionTDTypeEventArgs(spawnType));
        }
    }
}
