using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public GameObject destroyedVersion;
    public GameObject powerUpInside;

    
    public void desroyObject () {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Instantiate(powerUpInside, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public int health = 10;
    public int currentHealth = 10;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Weapon") {
            currentHealth = currentHealth - 5;
        }
        if (currentHealth <= 0) {
            desroyObject();
        }
    }
}
