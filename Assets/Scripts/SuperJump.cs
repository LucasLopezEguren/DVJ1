using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        Debug.Log("object: " + other.tag);
        if (other.gameObject.tag == "Player"){            
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.jumpForce = 30f;
            playerController.moveSpeed = 20f;
        }
    }

    void OnTriggerExit(Collider other) {
        Debug.Log("out: " + other.tag);
        if (other.tag == "Player"){            
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.jumpForce = 15f;
            playerController.moveSpeed = 10f;
        }
    }
}
