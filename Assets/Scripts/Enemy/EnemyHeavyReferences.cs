using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavyReferences : HeavyReferences
{
    [SerializeField]
    private Enemy black;
    [SerializeField]
    private Enemy iron;
    [SerializeField]
    private Enemy lead;
    [SerializeField]
    private Enemy magnesium;

    public Enemy Black { get { return black; } }
    public Enemy Iron { get { return iron; } }
    public Enemy Lead { get { return lead; } }
    public Enemy Magnesium { get { return magnesium; } }

    public override HeavyUnit GetHeavyReference(RedemptionTDType type)
    {
        var enemy = Black;
        switch(type)
        {
            case RedemptionTDType.BLACK_ENEMY:
                enemy = Black;
                break;

            case RedemptionTDType.IRON_ENEMY:
                enemy = Iron;
                break;

            case RedemptionTDType.LEAD_ENEMY:
                enemy = Lead;
                break;

            case RedemptionTDType.MAGNESIUM_ENEMY:
                enemy = Magnesium;
                break;

            default:
                break;
        }

        return enemy;
    }
}
