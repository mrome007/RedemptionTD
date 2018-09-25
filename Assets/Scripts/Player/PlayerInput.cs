using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour 
{
    private Vector2 mousePosition;

    protected virtual void Awake()
    {
        mousePosition = Vector2.zero;
    }

    protected virtual void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = screenPoint;

            var hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0);

            if(hit.collider.gameObject.layer == 8)
            {
                Debug.Log("Can Spawn" + hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("Cannot Spawn");
            }
        }
    }
}
