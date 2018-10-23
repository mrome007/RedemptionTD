using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasesOverseer : MonoBehaviour 
{
    [SerializeField]
    private List<RedemptionBase> bases;
    private int currentBasesCount;

    private void Awake()
    {
        currentBasesCount = bases.Count;
        RegisterBasesDestroyed();
    }

    private void RegisterBasesDestroyed()
    {
        foreach(var redemptionBase in bases)
        {
            redemptionBase.BaseDestroyed += HandleBaseDestroyed;
        }
    }

    private void HandleBaseDestroyed(object sender, RedemptionBaseDestroyedEventArgs e)
    {
        bases[e.Index].BaseDestroyed -= HandleBaseDestroyed;
        currentBasesCount--;

        if(currentBasesCount <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
