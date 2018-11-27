using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponResizer : MonoBehaviour 
{
    [SerializeField]
    private List<GameObject> weaponObjects;

    [SerializeField]
    private GameObject gatherObject;

    public const float upgradeSizeIncr = 0.2f;
    
    public void ResizeWeapon(int level)
    {
        var scaleWeapon = weaponObjects.FirstOrDefault().transform.localScale;
        var scaleGather = gatherObject.transform.localScale;

        var newScale = 1f + ((level - 1) * upgradeSizeIncr);
        scaleWeapon.x = scaleWeapon.y = scaleGather.x = scaleGather.y = newScale;
        weaponObjects.ForEach(weaponObject => weaponObject.transform.localScale = scaleWeapon);
        gatherObject.transform.localScale = scaleGather;
    }
}
