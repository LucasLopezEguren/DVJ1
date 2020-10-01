using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayer : MonoBehaviour{

    public GameObject player;

    void Start () {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            player.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            player.transform.parent = null;
        }
        
    }
}
