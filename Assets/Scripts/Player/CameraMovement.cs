using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
    [SerializeField]
    private Vector2 verticalBorder;

    [SerializeField]
    private Vector2 horizontalBorder;

    [SerializeField]
    private float movementSpeed;

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var position = transform.position;
        position += (new Vector3(horizontal, vertical, 0f) * movementSpeed * Time.deltaTime);
        position.x = Mathf.Clamp(position.x, horizontalBorder.x, horizontalBorder.y);
        position.y = Mathf.Clamp(position.y, verticalBorder.x, verticalBorder.y);

        transform.position = position;
    }
}
