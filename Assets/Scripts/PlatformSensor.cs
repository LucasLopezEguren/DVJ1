using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSensor : MonoBehaviour
{
    public AttachPlayer carrier;
    
    void OnTriggerEnter(Collider collider) {
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        if (rb != null && rb != carrier._rigidBody){
            carrier.AddRigidBody(rb);
        }
    }
    void OnTriggerExit(Collider collider) {
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        if (rb != null ){
            carrier.RemoveRigidBody(rb);
        }
    }
}
