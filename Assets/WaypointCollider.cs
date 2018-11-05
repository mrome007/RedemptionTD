using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCollider : MonoBehaviour 
{
    [SerializeField]
    private Transform position1;

    [SerializeField]
    private Transform position2;

    private BoxCollider2D wayPointCollider;
    private const float sizeOffset = 0.25f;
    
    private void Awake()
    {
        CreateBoxCollider();
    }

    private void CreateBoxCollider()
    {
        if(wayPointCollider != null)
        {
            return;
        }

        if(position1.position.x != position2.position.x && position1.position.y != position2.position.y)
        {
            return;
        }

        wayPointCollider = gameObject.AddComponent<BoxCollider2D>();

        var horizontal = position1.position.y == position2.position.y;
        var xDifference = Mathf.Abs(position1.position.x - position2.position.x) + sizeOffset;
        var yDifference = Mathf.Abs(position1.position.y - position2.position.y) + sizeOffset;
        wayPointCollider.size = horizontal ? new Vector2(xDifference, 0.25f) : new Vector2(0.25f, yDifference);

        var xMidPoint = (position1.position.x + position2.position.x) / 2f;
        var yMidPoint = (position1.position.y + position2.position.y) / 2f;
        var position = position1.position;
        if(horizontal)
        {
            position.x = xMidPoint;
        }
        else
        {
            position.y = yMidPoint;
        }
        transform.position = position;
    }
}
