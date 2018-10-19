using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCycler : MonoBehaviour 
{
    [SerializeField]
    private List<GameObject> weapons;
    
    private int currentWeaponIndex = 0;

    private void OnEnable()
    {
        weapons.ForEach(weapon => weapon.SetActive(false));
        weapons[currentWeaponIndex].SetActive(true);
    }
}
