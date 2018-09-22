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
    private RedemptionTDObjectPool objectPool;

    [SerializeField]
    private float timeBetweenWaves;

    [SerializeField]
    private List<RedemptionTDWave> waves;

    private int currentWaveIndex;

    private void Awake()
    {
        currentWaveIndex = 0;
        objectPool.ObjectPoolComplete += HandleObjectPoolComplete;
    }

    private void HandleObjectPoolComplete (object sender, EventArgs e)
    {
        objectPool.ObjectPoolComplete -= HandleObjectPoolComplete;
        StartRound();
    }

    private void StartRound()
    {
        currentWaveIndex = 0;
        RaiseRoundStart();
        InitiateWave();
    }

    private void InitiateWave()
    {
        var currentWave = waves[currentWaveIndex];
        currentWave.WaveEnded += HandleWaveEnded;

        currentWave.StartWave(objectPool);
    }

    private void HandleWaveEnded(object sender, EventArgs e)
    {
        var currentWave = waves[currentWaveIndex];
        currentWave.WaveEnded -= HandleWaveEnded;

        currentWaveIndex++;
        if(currentWaveIndex < waves.Count)
        {
            InitiateWave();
        }
        else
        {
            currentWaveIndex = 0;
            RaiseRoundEnd();
        }
    }

    public void RaiseRoundStart()
    {
        Debug.Log("Round Started");

        var handler = RoundStarted;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    public void RaiseRoundEnd()
    {
        Debug.Log("Round Ended");

        var handler = RoundEnded;
        if(handler != null)
        {
            handler(this, null);
        }
    }
}
