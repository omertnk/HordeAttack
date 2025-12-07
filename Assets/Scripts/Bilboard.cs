using System;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
   public Transform cam;
   private void LateUpdate()
   {
      if (cam)
      {
         transform.LookAt(transform.position + cam.forward);
      }
      
   }
}
