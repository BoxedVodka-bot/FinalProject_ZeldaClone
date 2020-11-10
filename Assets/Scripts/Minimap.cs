using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform Target;
    public Vector2 TransformPosition(Vector3 position){
        Vector3 offset = position-Target.position;
        Vector2 newPosition = new Vector2(offset.x, offset.z);
        return newPosition;
    }
    
    public Vector2 MoveInside(Vector2 point){
        Rect mapRect = GetComponent<RectTransform>().rect;
        point = Vector2.Max(point, mapRect.min);
        point = Vector2.Min(point, mapRect.max);
        return point;
    }
}
