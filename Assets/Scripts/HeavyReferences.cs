using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeavyReferences : MonoBehaviour
{
    [SerializeField]
    protected List<HeavyUnit> black;

    [SerializeField]
    protected List<HeavyUnit> iron;

    [SerializeField]
    protected List<HeavyUnit> lead;

    [SerializeField]
    protected List<HeavyUnit> magnesium;

    public virtual HeavyUnit GetHeavyReference(RedemptionTDType type)
    {
        return GetHeavyReference(type, 1);
    }

    public virtual HeavyUnit GetHeavyReference(RedemptionTDType type, int level)
    {
        if(level < 1 || level > black.Count)
        {
            level = 0;
        }
        else
        {
            level--;
        }

        HeavyUnit result = null;
        switch(type)
        {
            case RedemptionTDType.BLACK_WEAPON:
            case RedemptionTDType.BLACK_ENEMY:
            case RedemptionTDType.BLACK_RESOURCE:
            case RedemptionTDType.BLACK_RESOURCE_DROP:
            case RedemptionTDType.BLACK_BASE:
            case RedemptionTDType.BLACK_ENEMY_MID:
            case RedemptionTDType.BLACK_ENEMY_LARGE:
            case RedemptionTDType.BLACK_ENEMY_BOSS:
                result = black[level];
                break;

            case RedemptionTDType.IRON_WEAPON:
            case RedemptionTDType.IRON_ENEMY:
            case RedemptionTDType.IRON_RESOURCE:
            case RedemptionTDType.IRON_RESOURCE_DROP:
            case RedemptionTDType.IRON_BASE:
            case RedemptionTDType.IRON_ENEMY_MID:
            case RedemptionTDType.IRON_ENEMY_LARGE:
            case RedemptionTDType.IRON_ENEMY_BOSS:
                result = iron[level];
                break;

            case RedemptionTDType.LEAD_WEAPON:
            case RedemptionTDType.LEAD_ENEMY:
            case RedemptionTDType.LEAD_RESOURCE:
            case RedemptionTDType.LEAD_RESOURCE_DROP:
            case RedemptionTDType.LEAD_BASE:
            case RedemptionTDType.LEAD_ENEMY_MID:
            case RedemptionTDType.LEAD_ENEMY_LARGE:
            case RedemptionTDType.LEAD_ENEMY_BOSS:
                result = lead[level];
                break;

            case RedemptionTDType.MAGNESIUM_WEAPON:
            case RedemptionTDType.MAGNESIUM_ENEMY:
            case RedemptionTDType.MAGNESIUM_RESOURCE:
            case RedemptionTDType.MAGNESIUM_RESOURCE_DROP:
            case RedemptionTDType.MAGNESIUM_BASE:
            case RedemptionTDType.MAG_ENEMY_MID:
            case RedemptionTDType.MAG_ENEMY_LARGE:
            case RedemptionTDType.MAG_ENEMY_BOSS:
                result = magnesium[level];
                break;

            default:
                break;
        }

        return result;
    }
}
