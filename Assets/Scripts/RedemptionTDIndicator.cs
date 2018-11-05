using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionTDIndicator : MonoBehaviour 
{
    private Vector3 originalPosition;

    [SerializeField]
    private float offset;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if(InCameraView())
        {
            transform.position = originalPosition;
            return;
        }

        var bottomLeftCam = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane));
        var topRightCam = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));

        var xPos = Mathf.Clamp(originalPosition.x, bottomLeftCam.x + offset, topRightCam.x - offset);
        var yPos = Mathf.Clamp(originalPosition.y, bottomLeftCam.y + offset, topRightCam.y - offset);

        transform.position = new Vector3(xPos, yPos, -1f);
    }

    private bool InCameraView()
    {
        var bottomLeftCam = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane));
        var topRightCam = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));
        return originalPosition.x > bottomLeftCam.x + offset && originalPosition.x < topRightCam.x - offset
            && originalPosition.y > bottomLeftCam.y + offset && originalPosition.y < topRightCam.y - offset;
    }
}
