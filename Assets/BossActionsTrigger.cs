using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionsTrigger : MonoBehaviour
{
    public BossController bossController;
    public string action;
    private bool added = false;
    void OnTriggerEnter(Collider other) {
       if ((other.tag == "Player" || other.tag == "Weapon") && !added){
           bossController.SetPreferedAction(action);
       }
    }

    void OnTriggerExit(Collider other) {
       if ((other.tag == "Player" || other.tag == "Weapon")){
           bossController.RemoveAction(action);
           added = false;
       }
    }
}
