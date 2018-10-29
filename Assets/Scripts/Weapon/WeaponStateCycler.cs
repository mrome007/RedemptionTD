using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStateCycler : MonoBehaviour 
{
    [SerializeField]
    private BlastWeaponState blastState;

    [SerializeField]
    private GatherWeaponState gatherState;

    [SerializeField]
    private NullWeaponState nullState;

    public WeaponState CurrentWeaponState { get; private set; }

    public WeaponMode InitializeWeaponState(WeaponMode mode, float radius, RedemptionTDColor color)
    {
        if(mode == WeaponMode.GATHER)
        {
            var resourceLayer = 1 << LayerMask.NameToLayer("Resource");
            var hit = Physics2D.OverlapCircle(transform.position, radius, resourceLayer);

            if(hit != null && hit.GetComponent<LiteUnit>().HeavyReference.Color == color)
            {
                CurrentWeaponState = gatherState;
                CurrentWeaponState.EnterWeaponState(hit.GetComponent<ResourceLite>());
                return WeaponMode.GATHER;
            }
            else
            {
                CurrentWeaponState = nullState;
                CurrentWeaponState.EnterWeaponState();
                return WeaponMode.NONE;
            }
        }
        else
        {
            CurrentWeaponState  = blastState;
            CurrentWeaponState.EnterWeaponState();
            return WeaponMode.WAVE;
        }
    }

    protected virtual void OnEnable()
    {
        CurrentWeaponState = nullState;
        CurrentWeaponState.EnterWeaponState();
    }
}
