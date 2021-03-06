﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour 
{
    [SerializeField]
    protected WeaponLite weaponLite;
    
    [SerializeField]
    protected List<WeaponBehavior> weaponBehaviors;

    protected bool weaponActive = false;
    protected WaitForSeconds activeTimeWait = null;
    protected Coroutine ActivateWeaponCoroutine = null;

    protected virtual void EnemyDetected(GameObject en)
    {
        var enemy = en.GetComponentInParent<EnemyLite>();
        if(enemy == null)
        {
            return;
        }

        if(enemy.HeavyReference.Color == weaponLite.HeavyReference.Color && !weaponActive)
        {
            ActivateWeaponCoroutine = StartCoroutine(ActivateWeaponBehavior());
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        EnemyDetected(col.gameObject);
    }

    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        EnemyDetected(col.gameObject);
    }

    protected virtual IEnumerator ActivateWeaponBehavior()
    {
        if(activeTimeWait == null)
        {
            activeTimeWait = new WaitForSeconds((weaponLite.HeavyReference as Weapon).ActiveTime);
        }
        
        weaponActive = true;
        weaponBehaviors.ForEach(weaponBehavior => weaponBehavior.gameObject.SetActive(true));

        yield return activeTimeWait;

        weaponActive = false;
        weaponBehaviors.ForEach(weaponBehavior => weaponBehavior.gameObject.SetActive(false));
    }

    protected virtual void OnEnable()
    {
        weaponBehaviors.ForEach(weaponBehavior => weaponBehavior.gameObject.SetActive(false));
    }

    protected virtual void OnDisable()
    {
        if(ActivateWeaponCoroutine != null)
        {
            StopCoroutine (ActivateWeaponCoroutine);
        }
        weaponActive = false;
    }
}
