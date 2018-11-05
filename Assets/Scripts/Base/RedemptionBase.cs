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

    [SerializeField]
    private GameObject baseIndicator;

    [SerializeField]
    private float indicatorShowTime = 2f;

    private float currentHealth;
    private float indicatorTimer;
    private Coroutine indicatorRoutine = null;
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
            indicatorTimer = indicatorShowTime;
            PostBaseDamaged(damage);
        }
        else
        {
            damage /= 2f;
            currentHealth -= damage;
            indicatorTimer = indicatorShowTime;
            PostBaseDamaged(damage);
        }

        ShowBaseIndicator();

        if(currentHealth <= 0)
        {
            baseIndicator.SetActive(false);
            StopCoroutine(indicatorRoutine);
            indicatorRoutine = null;
            PostBaseDestroyed();
            gameObject.SetActive(false);
        }
    }

    private void ShowBaseIndicator()
    {
        if(indicatorRoutine != null)
        {
            return;
        }

        indicatorRoutine = StartCoroutine(ShowBaseIndicatorRoutine());
    }

    private IEnumerator ShowBaseIndicatorRoutine()
    {
        baseIndicator.SetActive(true);
        while(indicatorTimer > 0f)
        {
            indicatorTimer -= Time.deltaTime;
            yield return null;
        }

        baseIndicator.SetActive(false);
        indicatorRoutine = null;
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
