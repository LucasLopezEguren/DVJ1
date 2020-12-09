using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeController : MonoBehaviour
{
    public Collider platform1;
    public Collider platform2;
    public Collider platform3;
    public Collider platform1Trigger;
    public Collider platform2Trigger;
    public Collider platform3Trigger;
    public GameObject platform1GO;
    public GameObject platform2GO;
    public GameObject platform3GO;
    public GameObject[] flyingEnemies;

    void Start() {
        platform1.enabled = true;
        platform2.enabled = true;
        platform3.enabled = true;
        platform1Trigger.enabled = true;
        platform2Trigger.enabled = true;
        platform3Trigger.enabled = true;
    }

    void FixedUpdate() {
        if (Input.GetButtonDown("Jump")){
            StartCoroutine("EnableCollider");
        }
    }

    IEnumerator EnableCollider () {
        yield return new WaitForSeconds(0.01f);

        if (platform1GO.GetComponent<AttachPlayer>().rigidbodies.Count == 0) {
            platform1GO.gameObject.tag = "DeadEnemies";
            platform1.enabled = false;
            platform1Trigger.enabled = false;
        }

        if (platform2GO.GetComponent<AttachPlayer>().rigidbodies.Count == 0) {
            platform2GO.gameObject.tag = "DeadEnemies";
            platform2Trigger.enabled = false;
            platform2.enabled = false;
        }

        if (platform3GO.GetComponent<AttachPlayer>().rigidbodies.Count == 0) {
            platform3GO.gameObject.tag = "DeadEnemies";
            platform3Trigger.enabled = false;
            platform3.enabled = false;
        }
        yield return new WaitForSeconds(0.3f);
        platform1.enabled = true;
        platform2.enabled = true;
        platform3.enabled = true;
        platform1Trigger.enabled = true;
        platform2Trigger.enabled = true;
        platform3Trigger.enabled = true;
        platform1GO.gameObject.tag = "Ground";
        platform2GO.gameObject.tag = "Ground";
        platform3GO.gameObject.tag = "Ground";
    }
}
