using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blip : MonoBehaviour
{
   public Transform Target;

   Minimap map;
   RectTransform myRectTransform;
   public bool KeepInBounds = true;

   void Start(){
       map = GetComponentInParent<Minimap>();
       myRectTransform = GetComponent<RectTransform>();
   }

   void LateUpdate(){
       Vector2 newPosition = map.TransformPosition(Target.position);
        if(KeepInBounds){
            newPosition = map.MoveInside(newPosition);
        }
        myRectTransform.localPosition = newPosition;
   }

}
