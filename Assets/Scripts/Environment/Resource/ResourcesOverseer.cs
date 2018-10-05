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
    public static event EventHandler<ResourcesEventArgs> ResourceCountChanged;
    public static event EventHandler<ResourcesEventArgs> ResourceChangeFailed;

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
        IncreaseResourceEvent(0);
    }

    public static bool IncreaseResourceEvent(int count)
    {
        if(!CanChangeResourceCount(true, count))
        {
            PostResourceChangeFailed();
            return false;
        }

        resourceCountStatic += count;
        PostResourceChanged();
        return true;
    }

    public static bool DecreaseResourceEvent(int count)
    {
        if(!CanChangeResourceCount(false, count))
        {
            PostResourceChangeFailed();
            return false;
        }

        resourceCountStatic -= count;
        PostResourceChanged();
        return true;
    }

    private static void PostResourceChanged()
    {
        var handler = ResourceCountChanged;
        if(handler != null)
        {
            handler(instance, new ResourcesEventArgs(resourceCountStatic));
        }
    }

    private static void PostResourceChangeFailed()
    {
        var handler = ResourceChangeFailed;
        if(handler != null)
        {
            handler(instance, null);
        }
    }

    private static bool CanChangeResourceCount(bool increase, int count)
    {
        count *= increase ? 1 : -1;
        var result = resourceCountStatic + count;
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
