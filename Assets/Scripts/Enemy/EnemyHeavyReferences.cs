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

    public Enemy GetEnemyHeavyReference(RedemptionEnemyType enemyType)
    {
        var enemy = Black;
        switch(enemyType)
        {
            case RedemptionEnemyType.BLACK:
                enemy = Black;
                break;

            case RedemptionEnemyType.IRON:
                enemy = Iron;
                break;

            case RedemptionEnemyType.LEAD:
                enemy = Lead;
                break;

            case RedemptionEnemyType.MAGNESIUM:
                enemy = Magnesium;
                break;

            default:
                break;
        }

        return enemy;
    }
}
