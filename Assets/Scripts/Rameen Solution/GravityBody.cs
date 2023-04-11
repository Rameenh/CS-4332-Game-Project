using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour
{
    GravityAttractor planet;
   void Awake(){
       planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
       GetComponent<Rigidbody>().useGravity = false; //we want to use our own gravity instead
       GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
   }

   void FixedUpdate() { //like update but gets called at a constant rate/ physics
        planet.Attract(transform);
   }
}
