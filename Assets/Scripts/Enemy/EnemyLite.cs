using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLite : MonoBehaviour, IInitializable
{
    [SerializeField]
    private Enemy enemy;

    public Enemy HeavyEnemyReference { get { return enemy; } }

    #region Override IInitializable

    /// <summary>
    /// Initialize the enemy's Heavy Reference.
    /// </summary>
    /// <param name="obj">Object.</param>
    public void Initialize(object obj)
    {
        enemy = obj as Enemy;

        if(enemy == null)
        {
            return;
        }
    }

    #endregion
}
