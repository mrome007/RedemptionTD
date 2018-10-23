using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDropHeavyReferences : HeavyReferences
{
    [SerializeField]
    private ResourceDrop black;
    [SerializeField]
    private ResourceDrop iron;
    [SerializeField]
    private ResourceDrop lead;
    [SerializeField]
    private ResourceDrop magnesium;

    public ResourceDrop Black { get { return black; } }
    public ResourceDrop Iron { get { return iron; } }
    public ResourceDrop Lead { get { return lead; } }
    public ResourceDrop Magnesium { get { return magnesium; } }

    public override HeavyUnit GetHeavyReference(RedemptionTDType type)
    {
        var resourceDrop = Black;
        switch(type)
        {
            case RedemptionTDType.BLACK_RESOURCE_DROP:
                resourceDrop = Black;
                break;

            case RedemptionTDType.IRON_RESOURCE_DROP:
                resourceDrop = Iron;
                break;

            case RedemptionTDType.LEAD_RESOURCE_DROP:
                resourceDrop = Lead;
                break;

            case RedemptionTDType.MAGNESIUM_RESOURCE_DROP:
                resourceDrop = Magnesium;
                break;

            default:
                break;
        }

        return resourceDrop;
    }
}
