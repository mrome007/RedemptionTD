using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour 
{
    [SerializeField]
    protected WeaponLite weaponLite;

    public float DamageMultiplier { get; protected set; }

    protected virtual void OnEnable()
    {
        weaponLite.ObjectReturned += HandleObjectReturned;
    }

    protected virtual void OnDisable()
    {
        weaponLite.ObjectReturned -= HandleObjectReturned;
    }

    public virtual float DamageEnemy(RedemptionTDColor enemyColor)
    {
        if(weaponLite.HeavyReference.Color != enemyColor)
        {
            return 0f;
        }

        var weapon = weaponLite.HeavyReference as Weapon;
        var currentDamage = weapon.BaseDamage * weapon.DamageMultiplier;
        return currentDamage * DamageMultiplier;
    }

    protected virtual void HandleObjectReturned(object sender, ToOrFromPoolEventArgs e)
    {
        weaponLite.ObjectReturned -= HandleObjectReturned;
        gameObject.SetActive(false);
    }
}
