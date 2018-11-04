using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour 
{

    #region Inspector Data

    [SerializeField]
    private Waypoint nextWaypoint;

    #endregion

    public Waypoint NextWaypoint { get { return nextWaypoint; } }

    private GameObject enemyIndicator = null;
    public GameObject EnemyIndicator 
    { 
        get 
        { 
            return enemyIndicator; 
        } 
        set
        { 
            if(enemyIndicator != null)
            {
                return;
            }
            enemyIndicator = value; 
        } 
    }
}
