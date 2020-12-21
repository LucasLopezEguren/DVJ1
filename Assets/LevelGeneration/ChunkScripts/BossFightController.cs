using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightController : MonoBehaviour
{
    public Collider platform1;
    public Collider platform1Trigger;
    public GameObject platform1GO;

    void Start() {
        platform1.enabled = true;
        platform1Trigger.enabled = true;
    }

    void FixedUpdate() {
        if (Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") < 0){
            StartCoroutine("EnableCollider");
        }
    }

    IEnumerator EnableCollider () {
        yield return new WaitForSeconds(0.01f);
        platform1GO.gameObject.tag = "DeadEnemies";
        platform1.enabled = false;
        platform1Trigger.enabled = false;

        yield return new WaitForSeconds(0.3f);
        platform1.enabled = true;
        platform1Trigger.enabled = true;
        platform1GO.gameObject.tag = "Ground";
    }
}
