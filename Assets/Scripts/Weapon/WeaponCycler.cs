using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCycler : MonoBehaviour 
{
    [SerializeField]
    private List<GameObject> weapons;

    [SerializeField]
    private GameObject weaponObject;

    [SerializeField]
    private GameObject gatherObject;

    private void HideAllWeapons()
    {
        weapons.ForEach(weapon => weapon.SetActive(false));
    }

    private void ShowCurrentWeapon(int index)
    {
        HideAllWeapons();
        weapons[index].SetActive(true);
    }

    public void ShowCurrentWeapon(WeaponMode mode, bool canGather)
    {
        //TODO 0 index for now, will change when weapons increase.
        if(mode == WeaponMode.GATHER)
        {
            weaponObject.SetActive(false);
            gatherObject.SetActive(canGather);
            HideAllWeapons();
        }
        else
        {
            weaponObject.SetActive(true);
            gatherObject.SetActive(false);
            ShowCurrentWeapon(0);
        }
    }
}
