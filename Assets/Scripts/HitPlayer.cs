using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject idle;
    public PlayerController playerController;
    private bool alreadyHit = false;
    public int bossAttackDamage = 15;

    public void SetReadyToAttack() {
        alreadyHit = false;
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !alreadyHit) {
            playerController.TakeDamage(bossAttackDamage);
            alreadyHit = true;
        }
    }
}
