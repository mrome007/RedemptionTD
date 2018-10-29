using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCycler : MonoBehaviour 
{
    [SerializeField]
    private List<GameObject> weapons;

    private void HideAllWeapons()
    {
        weapons.ForEach(weapon => weapon.SetActive(false));
    }

    private void ShowCurrentWeapon(int index)
    {
        HideAllWeapons();
        weapons[index].SetActive(true);
    }

    public void ShowCurrentWeapon(WeaponMode mode)
    {
        //TODO 0 index for now, will change when weapons increase.
        if(mode != WeaponMode.GATHER)
        {
            ShowCurrentWeapon(0);
        }
    }
}
