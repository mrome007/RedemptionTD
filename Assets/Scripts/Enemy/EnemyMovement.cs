using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLite))]
public class EnemyMovement : MonoBehaviour, IInitializable
{
    public float Speed { get; set; }
    
    #region Public Data

    public event EventHandler MoveStarted;
    public event EventHandler MoveEnded;

    #endregion

    #region Private Data

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

    public virtual void Move()
    {
        StartCoroutine(MoveEnemyRoutine());
    }

    private IEnumerator MoveEnemyRoutine()
    {
        RaiseMoveStarted();

        var curr = currentPoint;

        while(curr.NextWaypoint != null)
        {
            var originalPosition = curr.transform.position;
            var targetPosition = curr.NextWaypoint.transform.position;

            var t = 0f;
            var incrRate = (Speed * Time.deltaTime) / Vector3.Distance(originalPosition, targetPosition);

            while(t < 1f)
            {
                transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
                t += incrRate;
                yield return null;
            }

            curr = curr.NextWaypoint;
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
