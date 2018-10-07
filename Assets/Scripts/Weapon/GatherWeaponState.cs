using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherWeaponState : WeaponState
{
    private ResourceLite currentResource;
    private float timer;
    private float timeToGather;
    
    public override void EnterWeaponState(object obj = null)
    {
        currentResource = (ResourceLite)obj;
        currentResource.ObjectReturned += HandleObjectReturned;
        timer = 0f;
        timeToGather = currentResource.GetTimeToGather();
    }
    
    public override void UpdateWeapon()
    {
        if(currentResource != null)
        {
            if(timer < timeToGather)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                currentResource.GetResource();
            }
        }
    }

    private void HandleObjectReturned(object sender, ToOrFromPoolEventArgs e)
    {
        currentResource.ObjectReturned -= HandleObjectReturned;
        currentResource = null;
    }
}
