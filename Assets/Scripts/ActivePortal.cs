using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePortal : MonoBehaviour
{
    public GameObject Portal;
    public GameObject forceFieldBack;
    public GameObject forceFieldFront;
    public bool triggederd = false;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && !triggederd) {
            Portal.SetActive(true);
            forceFieldBack.SetActive(true);
            forceFieldFront.SetActive(true);
            triggederd = true;
        }
    }
}
