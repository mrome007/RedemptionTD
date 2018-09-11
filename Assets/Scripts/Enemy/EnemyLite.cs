using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLite : MonoBehaviour 
{
    [SerializeField]
    private Enemy enemy;

    public Enemy EnemyReference { get { return enemy; } } 
}
