using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHeavyReferences : HeavyReferences
{
    [SerializeField]
    private List<Weapon> black;

    [SerializeField]
    private List<Weapon> iron;

    [SerializeField]
    private List<Weapon> lead;

    [SerializeField]
    private List<Weapon> magnesium;

    public override HeavyUnit GetHeavyReference(RedemptionTDType type)
    {
        Weapon result = null;
        switch(type)
        {
            case RedemptionTDType.BLACK_WEAPON:
                result = black.FirstOrDefault();
                break;

            case RedemptionTDType.IRON_WEAPON:
                result = iron.FirstOrDefault();
                break;

            case RedemptionTDType.LEAD_WEAPON:
                result = lead.FirstOrDefault();
                break;

            case RedemptionTDType.MAGNESIUM_WEAPON:
                result = magnesium.FirstOrDefault();
                break;

            default:
                break;
        }

        return result;
    }

    public HeavyUnit GetHeavyReference(RedemptionTDType type, int level)
    {
        Weapon result = null;
        switch(type)
        {
            case RedemptionTDType.BLACK_WEAPON:
                result = black[level - 1];
                break;

            case RedemptionTDType.IRON_WEAPON:
                result = iron[level - 1];
                break;

            case RedemptionTDType.LEAD_WEAPON:
                result = lead[level - 1];
                break;

            case RedemptionTDType.MAGNESIUM_WEAPON:
                result = magnesium[level - 1];
                break;

            default:
                break;
        }

        return result;
    }
}
