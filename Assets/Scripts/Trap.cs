using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private GameObject player;
    private float initialY;
    private GameObject spikes;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        initialY = transform.position.y;
        spikes = transform.GetChild(0).gameObject;
    }


    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            spikes.GetComponent<Rigidbody>().AddForce(new Vector3(0,100,0), ForceMode.VelocityChange);
        }
    }

    // Update is called once per frame
    void Update() {
        if (initialY < transform.position.y - 100){
            spikes.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,0), ForceMode.VelocityChange);
            Rearm();
        }
    }

    void Rearm() {

        if (transform.position.y != initialY){
            // spikes
        }
    }
}
