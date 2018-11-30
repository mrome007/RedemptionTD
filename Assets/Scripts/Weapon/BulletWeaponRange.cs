using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeaponRange : WeaponRange
{
    private WaitForSeconds timeBetweenBullets = new WaitForSeconds(0.1f);

    protected override IEnumerator ActivateWeaponBehavior()
    {
        if(activeTimeWait == null)
        {
            activeTimeWait = new WaitForSeconds((weaponLite.HeavyReference as Weapon).ActiveTime);
        }

        weaponActive = true;
        var numberOfBursts = weaponLite.HeavyReference.Level;

        var index = 0;
        for(var count = 0; count < numberOfBursts; count++)
        {
            weaponBehaviors[index++].gameObject.SetActive(true);
            yield return timeBetweenBullets;
            weaponBehaviors[index++].gameObject.SetActive(true);
            yield return timeBetweenBullets;
        }

        yield return activeTimeWait;

        foreach(var weaponBehavior in weaponBehaviors)
        {
            weaponBehavior.gameObject.SetActive(false);
        }
        weaponActive = false;
    }
}
