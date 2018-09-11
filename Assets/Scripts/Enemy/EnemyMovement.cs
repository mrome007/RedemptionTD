using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLite))]
public class EnemyMovement : MonoBehaviour
{
    #region Inspector Data

    [SerializeField]
    private Waypoint initialPoint;

    #endregion

    #region Public

    public event EventHandler MoveStarted;
    public event EventHandler MoveEnded;

    #endregion

    #region Private

    private EnemyLite enemy;
    private Waypoint currentPoint;

    #endregion

    private void Awake()
    {
        currentPoint = initialPoint;
        enemy = GetComponent<EnemyLite>();
    }

    private void Start()
    {
        Move();
    }

    public void Move()
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
            var incrRate = enemy.EnemyReference.Speed * Time.deltaTime;

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
