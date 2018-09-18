using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionTDRound : MonoBehaviour 
{
    #region Events

    public event EventHandler RoundStarted;

    public event EventHandler RoundEnded;

    #endregion

    [SerializeField]
    private RedemptionEnemyObjectPool objectPool;

    [SerializeField]
    private float timeBetweenWaves;

    [SerializeField]
    private List<RedemptionTDWave> waves;

    private void Awake()
    {
        objectPool.ObjectPoolComplete += HandleObjectPoolComplete;
    }

    private void HandleObjectPoolComplete (object sender, EventArgs e)
    {
        objectPool.ObjectPoolComplete -= HandleObjectPoolComplete;
        StartRound();
    }

    private void StartRound()
    {

    }

    public void RaiseRoundStart()
    {
        var handler = RoundStarted;
        if(handler != null)
        {
            handler(this, null);
        }

        Debug.Log("Round Started");
    }

    public void RaiseRoundEnd()
    {
        var handler = RoundEnded;
        if(handler != null)
        {
            handler(this, null);
        }

        Debug.Log("Round Ended");
    }
}
