using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionBase : MonoBehaviour 
{
    public event EventHandler BaseDestroyed;
    
    [SerializeField]
    private float health;

    [SerializeField]
    private RedemptionTDColor color;

    [SerializeField]
    private RedemptionTDType type;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = health;
    }

    public void DamageBase(RedemptionTDColor enemyColor, float damage)
    {
        if(enemyColor == color)
        {
            currentHealth -= damage;
        }
        else
        {
            damage /= 2f;
            currentHealth -= damage;
        }

        if(currentHealth <= 0)
        {
            PostBaseDestroyed();
            gameObject.SetActive(false);
        }
    }

    private void PostBaseDestroyed()
    {
        var handler = BaseDestroyed;
        if(handler != null)
        {
            handler(this, null);
        }
    }
}
