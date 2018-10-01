using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceHeavyReferences : HeavyReferences
{
    [SerializeField]
    private List<Resource> black;

    [SerializeField]
    private List<Resource> iron;

    [SerializeField]
    private List<Resource> lead;

    [SerializeField]
    private List<Resource> magnesium;

    public override HeavyUnit GetHeavyReference(RedemptionTDType type)
    {
        Resource result = null;
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
        if(level < 1 || level > 3)
        {
            level = 0;
        }
        else
        {
            level = level - 1;
        }

        Resource result = null;
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
