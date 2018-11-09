using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BasesOverseer : MonoBehaviour 
{
    public System.Action BasesDestroyed;

    [SerializeField]
    private Text hitPointsText;
    
    [SerializeField]
    private List<RedemptionBase> bases;

    private int currentBasesCount;
    private StringBuilder hitPointsbuffer;
    private const string hitPointsFormat = "{0}/{1}";
    private float overallHealth;

    private void Awake()
    {
        currentBasesCount = bases.Count;
        SetHitPointsText();
        RegisterBasesDestroyed();
        RegisterBasesDamaged();
    }

    private void RegisterBasesDestroyed()
    {
        foreach(var redemptionBase in bases)
        {
            redemptionBase.BaseDestroyed += HandleBaseDestroyed;
        }
    }

    private void RegisterBasesDamaged()
    {
        foreach(var redemptionBase in bases)
        {
            redemptionBase.BaseDamaged += HandleBaseDamaged;
        }
    }

    private void UnRegisterBasesDestroyed()
    {
        foreach(var redemptionBase in bases)
        {
            redemptionBase.BaseDestroyed -= HandleBaseDestroyed;
        }
    }

    private void UnRegisterBasesDamaged()
    {
        foreach(var redemptionBase in bases)
        {
            redemptionBase.BaseDamaged -= HandleBaseDamaged;
        }
    }

    private void HandleBaseDamaged(object sender, RedemptionBaseDamagedEventArgs e)
    {
        UpdateHitPointsText(e.Damage);
    }

    private void HandleBaseDestroyed(object sender, RedemptionBaseDestroyedEventArgs e)
    {
        bases[e.Index].BaseDestroyed -= HandleBaseDestroyed;
        bases[e.Index].BaseDamaged -= HandleBaseDamaged;
        currentBasesCount--;

        if(currentBasesCount <= 0)
        {
            BasesDestroyed.Invoke();
        }
    }

    private void SetHitPointsText()
    {
        if(hitPointsbuffer != null)
        {
            return;
        }
        overallHealth = bases.Sum(redemptionBase => redemptionBase.Health);
        hitPointsbuffer = new StringBuilder();
        hitPointsbuffer.AppendFormat(hitPointsFormat, overallHealth, overallHealth);
        hitPointsText.text = hitPointsbuffer.ToString();
    }

    private void UpdateHitPointsText(float damage)
    {
        hitPointsbuffer.Length = 0;
        overallHealth -= damage;
        if(overallHealth < 0f)
        {
            overallHealth = 0f;
        }
        hitPointsbuffer.AppendFormat(hitPointsFormat, overallHealth, overallHealth);
        hitPointsText.text = hitPointsbuffer.ToString();
    }
}
