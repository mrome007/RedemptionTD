using System;
using UnityEngine;

public class ResourcesOverseer : MonoBehaviour
{
    #region Inspector Data

    [SerializeField]
    private int resourceCount;

    [SerializeField]
    private int maxResourceCount;

    #endregion

    private static ResourcesOverseer instance;
    private static int resourceCountStatic;
    private static int maxResourceCountStatic;
    public static event EventHandler<ResourcesEventArgs> IncreaseResourceCount;
    public static event EventHandler<ResourcesEventArgs> DecreaseResourceCount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = GameObject.FindObjectOfType<ResourcesOverseer>();
        }
        else
        {
            Debug.LogError("Only one instance.");
        }
        DontDestroyOnLoad(instance);

        resourceCountStatic = resourceCount;
        maxResourceCountStatic = maxResourceCount;
    }

    private void Start()
    {
        ChangeIncreaseResourceEvent(0);
    }

    public static void ChangeIncreaseResourceEvent(int count)
    {
        if(!CanChangeResourceCount(true, count))
        {
            return;
        }

        resourceCountStatic += count;
        
        var handler = IncreaseResourceCount;
        if(handler != null)
        {
            handler(null, new ResourcesEventArgs(resourceCountStatic));
        }
    }

    public static void ChangeDecreaseResourceEvent(int count)
    {
        if(!CanChangeResourceCount(false, count))
        {
            return;
        }

        resourceCountStatic -= count;

        var handler = DecreaseResourceCount;
        if(handler != null)
        {
            handler(null, new ResourcesEventArgs(resourceCountStatic));
        }
    }

    private static bool CanChangeResourceCount(bool increase, int count)
    {
        var result = resourceCountStatic + (count * (increase ? 1 : -1));
        if(increase)
        {
            return result <= maxResourceCountStatic;
        }
        else
        {
            return result >= 0;
        }
    }
}
