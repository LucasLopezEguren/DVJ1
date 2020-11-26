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
        Debug.Log(triggederd);
        if (other.gameObject.tag == "Player" && !triggederd) {
            Debug.Log("Activate Portal!");
            Portal.SetActive(true);
            forceFieldBack.SetActive(true);
            forceFieldFront.SetActive(true);
            triggederd = true;
        }
    }
}
