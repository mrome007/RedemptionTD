using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCycler : MonoBehaviour 
{
    [SerializeField]
    private List<GameObject> weapons;

    [SerializeField]
    private List<GameObject> gathers;

    private void HideAllWeapons()
    {
        weapons.ForEach(weapon => weapon.SetActive(false));
        gathers.ForEach(gather => gather.SetActive(false));
    }

    private void ShowCurrentWeapon(int index)
    {
        HideAllWeapons();
        weapons[index].SetActive(true);
    }

    private void ShowCurrentGather(int index)
    {
        HideAllWeapons();
        gathers[index].SetActive(true);
    }

    public void ShowCurrentWeapon(WeaponMode mode)
    {
        //TODO 0 index for now, will change when weapons increase.
        if(mode == WeaponMode.GATHER)
        {
            ShowCurrentGather(0);
        }
        else
        {
            ShowCurrentWeapon(0);
        }
    }
}
