using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : HeavyUnit
{
    #region Inspector Data

    [SerializeField]
    private int totalResource;

    [SerializeField]
    private int givenResource;
   
    [SerializeField]
    private float timeToGiveResource;

    [Tooltip("Number between 1 and 100. This is the rate at which no resource will be given.")]
    [SerializeField]
    private int bustRate;

    #endregion

    public int TotalResource { get { return totalResource; } }
    public int GivenResource { get { return givenResource; } }
    public float TimeToGiveResource { get { return timeToGiveResource; } }
    public int BustRate { get { return bustRate; } }
}
