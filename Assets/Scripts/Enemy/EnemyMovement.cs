﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLite))]
public class EnemyMovement : MonoBehaviour, IInitializable
{
    #region Public Data

    public event EventHandler MoveStarted;
    public event EventHandler MoveEnded;

    #endregion

    #region Private Data

    private EnemyLite enemy;

    [SerializeField]
    private Waypoint currentPoint;

    #endregion

    #region Override IInitializable

    /// <summary>
    /// Initialize the initial Waypoint
    /// </summary>
    /// <param name="obj">Object.</param>
    public void Initialize(object obj)
    {
        currentPoint = obj as Waypoint;

        if(currentPoint == null)
        {
            return;
        }

        Move();
    }

    #endregion

    private void Awake()
    {
        enemy = GetComponent<EnemyLite>();
    }
        
    private void Move()
    {
        StartCoroutine(MoveEnemyRoutine());
    }

    private IEnumerator MoveEnemyRoutine()
    {
        RaiseMoveStarted();

        while(currentPoint.NextWaypoint != null)
        {
            var originalPosition = currentPoint.transform.position;
            var targetPosition = currentPoint.NextWaypoint.transform.position;

            var t = 0f;
            var incrRate = (enemy.HeavyEnemyReference.Speed * Time.deltaTime) / Vector3.Distance(originalPosition, targetPosition);

            while(t < 1f)
            {
                transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
                t += incrRate;
                yield return null;
            }

            currentPoint = currentPoint.NextWaypoint;
        }

        RaiseMoveEnded();
    }

    private void RaiseMoveStarted()
    {
        var handler = MoveStarted;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    private void RaiseMoveEnded()
    {
        var handler = MoveEnded;
        if(handler != null)
        {
            handler(this, null);
        }
    }
}
