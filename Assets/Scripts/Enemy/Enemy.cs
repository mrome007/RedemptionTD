using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    #region Inspector Data

    [SerializeField]
    private float speed;

    #endregion

    public float Speed { get { return speed; } }
}
