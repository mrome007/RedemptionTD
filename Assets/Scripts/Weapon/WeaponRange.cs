using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour 
{
    [SerializeField]
    private WeaponLite weaponLite;
    
    [SerializeField]
    private WeaponBehavior weaponBehavior;

    private bool weaponActive = false;
    private WaitForSeconds activeTimeWait = null;

    private void EnemyDetected(GameObject en)
    {
        var enemy = en.GetComponentInParent<EnemyLite>();
        if(enemy == null)
        {
            return;
        }

        if(enemy.HeavyReference.Color == weaponLite.HeavyReference.Color && !weaponActive)
        {
            StartCoroutine(ActivateWeaponBehavior());
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

    private IEnumerator ActivateWeaponBehavior()
    {
        if(activeTimeWait == null)
        {
            activeTimeWait = new WaitForSeconds((weaponLite.HeavyReference as Weapon).ActiveTime);
        }
        
        weaponActive = true;
        weaponBehavior.gameObject.SetActive(weaponActive);

        yield return activeTimeWait;

        weaponActive = false;
        weaponBehavior.gameObject.SetActive(weaponActive);
    }
}
