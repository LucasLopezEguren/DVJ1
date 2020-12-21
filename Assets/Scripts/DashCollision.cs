using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCollision : MonoBehaviour
{
    public PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (playerController.skillTree.skills.hittingDash && playerController.isDashing && other.tag == "Enemy")
        {
            Debug.Log("hit");
            other.GetComponent<DamageController>().TakeDamage(playerController.playerAttackEvent.damage);
        }
    }    
}
