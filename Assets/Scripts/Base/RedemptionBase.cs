using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionBase : MonoBehaviour 
{
    public event EventHandler<RedemptionBaseDestroyedEventArgs> BaseDestroyed;
    public event EventHandler<RedemptionBaseDamagedEventArgs> BaseDamaged;
    
    [SerializeField]
    private float health;

    [SerializeField]
    private RedemptionTDColor color;

    [SerializeField]
    private RedemptionTDType type;

    [SerializeField]
    private int baseIndex;

    private float currentHealth;
    private RedemptionBaseDestroyedEventArgs destroyedArgs;
    private RedemptionBaseDamagedEventArgs damagedArgs;
    public float Health { get{ return health; } }

    private void Awake()
    {
        currentHealth = health;
        destroyedArgs = new RedemptionBaseDestroyedEventArgs(baseIndex);
        damagedArgs = new RedemptionBaseDamagedEventArgs(0f);
    }

    public void DamageBase(RedemptionTDColor enemyColor, float damage)
    {
        if(enemyColor == color)
        {
            currentHealth -= damage;
            PostBaseDamaged(damage);
        }
        else
        {
            damage /= 2f;
            currentHealth -= damage;
            PostBaseDamaged(damage);
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
            handler(this, destroyedArgs);
        }
    }

    private void PostBaseDamaged(float damage)
    {
        var handler = BaseDamaged;
        if(handler != null)
        {
            damagedArgs.Damage = damage;
            handler(this, damagedArgs);
        }
    }
}
