using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUIController : MonoBehaviour 
{    
    [SerializeField]
    private Text resourceText;

    private void Awake()
    {
        ResourcesOverseer.ResourceCountChanged += HandleResourceCountChange;
    }
        
    private void OnDestroy()
    {
        ResourcesOverseer.ResourceCountChanged -= HandleResourceCountChange;
    }

    private void HandleResourceCountChange(object sender, ResourcesEventArgs e)
    {
        resourceText.text = e.ResourceCount.ToString();
    }
}
