using System;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
   private Transform mainCameraTransform;

   void Start()
   {
      if (Camera.main != null)
      {
         mainCameraTransform = Camera.main.transform;
      }
   }

   
   void LateUpdate()
   {
      if (mainCameraTransform == null) return;
      
      transform.forward = mainCameraTransform.forward;
   }
}
