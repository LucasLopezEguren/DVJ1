using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Rigidbody Rb;
    public float fowardForce = 2000f;
    public float sideForce = 500f;

    // Update is called once per frame
    void Update() {
        if(Input.GetKey("d")){
            Rb.AddForce(sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if(Input.GetKey("a")){
            Rb.AddForce(-sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if(Input.GetKey("space")){
            Rb.AddForce(0, -fowardForce * Time.deltaTime, 0, ForceMode.VelocityChange);
        }
    }
}
