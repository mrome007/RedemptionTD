using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionTDObjectPool : MonoBehaviour 
{
    #region Public Data

    public event EventHandler ObjectPoolBegin;

    public event EventHandler ObjectPoolComplete;

    #endregion
    
    #region Inspector Data

    [SerializeField]
    private List<RedemptionTDUnit> unitsList;

    [SerializeField]
    private List<RedemptionTDHeavyReferences> heavyList;

    [SerializeField]
    private Transform unitsPoolParent;

    #endregion

    #region Private Data

    private Dictionary<RedemptionTDType, LiteUnit> unitsDictionary;
    private Dictionary<RedemptionTDType, List<LiteUnit>> unitsPool;
    private Dictionary<RedemptionTDType, int> unitsPoolIndex;

    private Dictionary<RedemptionTDHeavyType, HeavyReferences> heavyDictionary;

    #endregion

    private void Awake()
    {
        InitializeObjectPoolDictionary();
        InitializeHeavyDictionary();
        CreateRedemptionObjectPool();
    }

    public IEnumerable<LiteUnit> GetUnits(RedemptionTDType type, int numberToSpawn, int level = 1)
    {
        for(int count = 0; count < numberToSpawn; count++)
        {
            if(!unitsPool.ContainsKey(type))
            {
                break;
            }
                
            var unitsPoolList = unitsPool[type];
            var unitIndex = unitsPoolIndex[type];

            if(unitIndex < unitsPoolList.Count)
            {
                var unit = unitsPoolList[unitIndex];
                var heavyReference = GetHeavyReference(type);
                unit.Initialize(heavyReference.GetHeavyReference(type, level));
                unitsPoolList[unitIndex] = null;
                unitsPoolIndex[type]++;
                unit.gameObject.SetActive(true);
                unit.transform.parent = null;
                yield return unit;
            }
            else
            {
                break;
            }
        }
    }

    public RedemptionTDType GetResourceDropType(RedemptionTDColor color)
    {
        var result = RedemptionTDType.BLACK_RESOURCE_DROP;
        switch(color)
        {
            case RedemptionTDColor.BLACK:
                result = RedemptionTDType.BLACK_RESOURCE_DROP;
                break;

            case RedemptionTDColor.IRON:
                result = RedemptionTDType.IRON_RESOURCE_DROP;
                break;

            case RedemptionTDColor.LEAD:
                result = RedemptionTDType.LEAD_RESOURCE_DROP;
                break;

            case RedemptionTDColor.MAGNESIUM:
                result = RedemptionTDType.MAGNESIUM_RESOURCE_DROP;
                break;
            default:
                break;
        }

        return result;
    }

    private void ReturnUnit(RedemptionTDType type, LiteUnit unit)
    {
        var unitsPoolList = unitsPool[type];
        var unitsIndex = unitsPoolIndex[type];

        if(unitsIndex > 0)
        {
            unit.transform.parent = unitsPoolParent;
            unit.transform.position = Vector3.zero;
            unitsPoolIndex[type]--;
            unitsIndex = unitsPoolIndex[type];
            unitsPoolList[unitsIndex] = unit;
            unit.gameObject.SetActive(false);
        }
    }

    #region Helpers

    private void CreateRedemptionObjectPool()
    {
        if(unitsPool != null)
        {
            return;
        }

        RaiseObjectPoolBegin();

        unitsPool = new Dictionary<RedemptionTDType, List<LiteUnit>>();
        unitsPoolIndex = new Dictionary<RedemptionTDType, int>();

        foreach(var unit in unitsList)
        {
            if(unitsPool.ContainsKey(unit.Type))
            {
                continue;
            }

            unitsPool.Add(unit.Type, new List<LiteUnit>());
            unitsPoolIndex.Add(unit.Type, 0);

            for(int count = 0; count < unit.PoolAmount; count++)
            {
                var liteUnit = (LiteUnit)Instantiate(unitsDictionary[unit.Type], transform.position, Quaternion.identity);
                var heavyReference = GetHeavyReference(unit.Type);
                liteUnit.Initialize(heavyReference.GetHeavyReference(unit.Type));

                liteUnit.transform.parent = unitsPoolParent;
                liteUnit.transform.position = Vector3.zero;

                unitsPool[unit.Type].Add(liteUnit);
                liteUnit.gameObject.SetActive(false);
                liteUnit.ObjectReturned += HandleLiteUnitReturned;
            }
        }
            
        RaiseObjectPoolComplete();
    }

    private void HandleLiteUnitReturned(object sender, ToOrFromPoolEventArgs e)
    {
        var liteUnit = sender as LiteUnit;
        if(liteUnit != null)
        {
            ReturnUnit(liteUnit.HeavyReference.Type, liteUnit);
        }
    }

    private void InitializeObjectPoolDictionary()
    {
        if(unitsDictionary != null)
        {
            return;
        }
        
        unitsDictionary = new Dictionary<RedemptionTDType, LiteUnit>();

        foreach(var unit in unitsList)
        {
            if(unitsDictionary.ContainsKey(unit.Type))
            {
                continue;
            }

            unitsDictionary.Add(unit.Type, unit.LiteObject);
        }
    }

    private void InitializeHeavyDictionary()
    {
        if(heavyDictionary != null)
        {
            return;
        }

        heavyDictionary = new Dictionary<RedemptionTDHeavyType, HeavyReferences>();

        foreach(var heavy in heavyList)
        {
            if(heavyDictionary.ContainsKey(heavy.HeavyType))
            {
                continue;
            }

            heavyDictionary.Add(heavy.HeavyType, heavy.HeavyReference);
        }
    }

    public HeavyUnit GetHeavyUnit(RedemptionTDType type, int level = 1)
    {
        var heavyReferences = GetHeavyReference(type);
        return heavyReferences.GetHeavyReference(type, level);
    }


    private HeavyReferences GetHeavyReference(RedemptionTDType type)
    {
        HeavyReferences result = null;
        switch(type)
        {
            case RedemptionTDType.BLACK_ENEMY:
            case RedemptionTDType.IRON_ENEMY:
            case RedemptionTDType.LEAD_ENEMY:
            case RedemptionTDType.MAGNESIUM_ENEMY:
                result = heavyDictionary[RedemptionTDHeavyType.ENEMY];
                break;

            case RedemptionTDType.BLACK_WEAPON:
            case RedemptionTDType.IRON_WEAPON:
            case RedemptionTDType.LEAD_WEAPON:
            case RedemptionTDType.MAGNESIUM_WEAPON:
                result = heavyDictionary[RedemptionTDHeavyType.WEAPON];
                break;

            case RedemptionTDType.BLACK_RESOURCE_DROP:
            case RedemptionTDType.IRON_RESOURCE_DROP:
            case RedemptionTDType.LEAD_RESOURCE_DROP:
            case RedemptionTDType.MAGNESIUM_RESOURCE_DROP:
                result = heavyDictionary[RedemptionTDHeavyType.RESOURCEDROP];
                break;

            default:
                break;
        }

        return result;
    }

    private void RaiseObjectPoolBegin()
    {
        Debug.Log("Object Pool Begin");

        var handler = ObjectPoolBegin;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    private void RaiseObjectPoolComplete()
    {
        Debug.Log("Object Pool Complete");

        var handler = ObjectPoolComplete;
        if(handler != null)
        {
            handler(this, null);
        }
    }

    #endregion
}

[Serializable]
public class RedemptionTDUnit
{
    public RedemptionTDType Type;
    public LiteUnit LiteObject;
    public int PoolAmount;
}

[Serializable]
public class RedemptionTDHeavyReferences
{
    public RedemptionTDHeavyType HeavyType;
    public HeavyReferences HeavyReference;
}

public enum RedemptionTDType
{
    BLANK,
    BLACK_ENEMY,
    IRON_ENEMY,
    LEAD_ENEMY,
    MAGNESIUM_ENEMY,

    BLACK_WEAPON,
    IRON_WEAPON,
    LEAD_WEAPON,
    MAGNESIUM_WEAPON,

    BLACK_RESOURCE,
    IRON_RESOURCE,
    LEAD_RESOURCE,
    MAGNESIUM_RESOURCE,

    BLACK_RESOURCE_DROP,
    IRON_RESOURCE_DROP,
    LEAD_RESOURCE_DROP,
    MAGNESIUM_RESOURCE_DROP,

    BLACK_BASE,
    IRON_BASE,
    LEAD_BASE,
    MAGNESIUM_BASE,

    BLACK_ENEMY_MID,
    BLACK_ENEMY_LARGE,
    BLACK_ENEMY_BOSS,

    IRON_ENEMY_MID,
    IRON_ENEMY_LARGE,
    IRON_ENEMY_BOSS,

    LEAD_ENEMY_MID,
    LEAD_ENEMY_LARGE,
    LEAD_ENEMY_BOSS,

    MAG_ENEMY_MID,
    MAG_ENEMY_LARGE,
    MAG_ENEMY_BOSS
}

public enum RedemptionTDHeavyType
{
    NONE,
    ENEMY,
    WEAPON,
    RESOURCE,
    RESOURCEDROP
}

public enum RedemptionTDColor
{
    NONE,
    BLACK,
    IRON,
    LEAD,
    MAGNESIUM
}