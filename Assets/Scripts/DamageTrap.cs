using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    private bool damageDone = false;
   void OnTriggerEnter(Collider other) {
       if (other.gameObject.tag == "Player" && !damageDone) {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(25);
            damageDone = true;
       }
   }

   void OnTriggerExit(Collider other) {
       damageDone = false;
   }
}
