using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayer : MonoBehaviour{

    public GameObject player;
    public List<GameObject> weapons;

    void Start () {
        player = GameObject.Find("Player");
        foreach (GameObject weapon in GameObject.FindGameObjectsWithTag("Weapon")){
            weapons.Add(weapon);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (!weapons.Contains(other.gameObject)) {
            other.gameObject.transform.parent = transform;
        }
    }
    void OnTriggerExit(Collider other) {
        if (!weapons.Contains(other.gameObject)) {
            other.gameObject.transform.parent = null;
        }
    }
}
