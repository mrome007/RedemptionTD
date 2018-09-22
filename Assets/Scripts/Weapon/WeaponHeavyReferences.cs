using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHeavyReferences : MonoBehaviour 
{
    [SerializeField]
    private List<Weapon> black;

    [SerializeField]
    private List<Weapon> iron;

    [SerializeField]
    private List<Weapon> lead;

    [SerializeField]
    private List<Weapon> magnesium;

    public Weapon GetWeaponHeavyReference(RedemptionTDType type, int level)
    {
        Weapon result = null;
        switch(type)
        {
            case RedemptionTDType.BLACK:
                result = black[level - 1];
                break;

            case RedemptionTDType.IRON:
                result = iron[level - 1];
                break;

            case RedemptionTDType.LEAD:
                result = lead[level - 1];
                break;

            case RedemptionTDType.MAGNESIUM:
                result = lead[level - 1];
                break;

            default:
                break;
        }

        return result;
    }
}
