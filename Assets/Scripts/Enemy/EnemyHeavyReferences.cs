using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavyReferences : MonoBehaviour 
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

    public Enemy GetEnemyHeavyReference(RedemptionTDType enemyType)
    {
        var enemy = Black;
        switch(enemyType)
        {
            case RedemptionTDType.BLACK:
                enemy = Black;
                break;

            case RedemptionTDType.IRON:
                enemy = Iron;
                break;

            case RedemptionTDType.LEAD:
                enemy = Lead;
                break;

            case RedemptionTDType.MAGNESIUM:
                enemy = Magnesium;
                break;

            default:
                break;
        }

        return enemy;
    }
}
