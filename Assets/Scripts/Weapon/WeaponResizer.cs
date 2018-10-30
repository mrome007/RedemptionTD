using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponResizer : MonoBehaviour 
{
    [SerializeField]
    private GameObject weaponObject;

    [SerializeField]
    private GameObject gatherObject;

    public const float upgradeSizeIncr = 0.15f;
    
    public void ResizeWeapon(int level)
    {
        var scaleWeapon = weaponObject.transform.localScale;
        var scaleGather = gatherObject.transform.localScale;

        var newScale = 1f + ((level - 1) * upgradeSizeIncr);
        scaleWeapon.x = scaleWeapon.y = scaleGather.x = scaleGather.y = newScale;
        weaponObject.transform.localScale = scaleWeapon;
        gatherObject.transform.localScale = scaleGather;
    }
}
