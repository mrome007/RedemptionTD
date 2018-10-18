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
    private WaitForSeconds activeTimeWait;

    private void Awake()
    {
        activeTimeWait = new WaitForSeconds(weaponBehavior.ActiveTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        var enemy = col.GetComponent<EnemyLite>();
        if(enemy.HeavyReference.Color == weaponLite.HeavyReference.Color && !weaponActive)
        {
            StartCoroutine(ActivateWeaponBehavior());
        }
    }

    private IEnumerator ActivateWeaponBehavior()
    {
        weaponActive = true;
        weaponBehavior.gameObject.SetActive(weaponActive);

        yield return activeTimeWait;

        weaponActive = false;
        weaponBehavior.gameObject.SetActive(weaponActive);
    }
}
