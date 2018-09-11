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
}
