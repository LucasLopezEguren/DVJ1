using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealth : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.currentHealth + 20 > playerController.maxHealth){
                playerController.currentHealth = playerController.maxHealth;
            } else {
                playerController.currentHealth += 20;
                playerController.healthBar.SetHealth(playerController.currentHealth);
            }
            Destroy(gameObject);
        }
    }
}
