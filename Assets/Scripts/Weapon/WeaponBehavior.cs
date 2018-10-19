using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour 
{
    [SerializeField]
    private WeaponLite weaponLite;

    public float DamageMultiplier { get; private set; }

    private void OnEnable()
    {
        weaponLite.ObjectReturned += HandleObjectReturned;
    }

    private void OnDisable()
    {
        weaponLite.ObjectReturned -= HandleObjectReturned;
    }

    public float DamageEnemy(RedemptionTDColor enemyColor)
    {
        if(weaponLite.HeavyReference.Color != enemyColor)
        {
            return 0f;
        }

        var weapon = weaponLite.HeavyReference as Weapon;
        var currentDamage = weapon.BaseDamage * weapon.DamageMultiplier;
        return currentDamage * DamageMultiplier;
    }

    private void HandleObjectReturned(object sender, ToOrFromPoolEventArgs e)
    {
        weaponLite.ObjectReturned -= HandleObjectReturned;
        gameObject.SetActive(false);
    }
}
