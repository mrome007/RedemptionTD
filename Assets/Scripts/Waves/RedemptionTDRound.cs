using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private BasesOverseer basesOverseer;

    [SerializeField]
    private RedemptionTDTimer timer;

    private int currentWaveIndex;

    private void Awake()
    {
        currentWaveIndex = 0;
        objectPool.ObjectPoolComplete += HandleObjectPoolComplete;
        basesOverseer.BasesDestroyed += HandleBasesDestroyed;
    }

    private void HandleObjectPoolComplete (object sender, EventArgs e)
    {
        objectPool.ObjectPoolComplete -= HandleObjectPoolComplete;
        timer.StartTimer();
        StartRound();
    }

    private void HandleBasesDestroyed()
    {
        EndRound();
    }

    private void EndRound()
    {
        basesOverseer.BasesDestroyed -= HandleBasesDestroyed;
        objectPool.ObjectPoolComplete -= HandleObjectPoolComplete;

        timer.StopTimer();
        Debug.Log("^^ " + timer.CurrentTime);
        SceneManager.LoadScene(0);
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
            EndRound();
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
