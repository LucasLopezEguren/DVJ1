using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    private float enterX;
    private float enterY;
    private int playerCurrentHp;
    private float jumpForce;
    private float moveSpeed;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){            
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerCurrentHp = playerController.currentHealth;
            enterX = other.transform.position.x;
            enterY = other.transform.position.y;
            jumpForce = playerController.jumpForce;
            moveSpeed = playerController.moveSpeed;
            playerController.jumpForce = 35f;
            playerController.moveSpeed = 25f;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player"){            
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.jumpForce = jumpForce;
            playerController.moveSpeed = moveSpeed;
            if (playerController.currentHealth >= playerCurrentHp && enterX < other.transform.position.x && enterY < other.transform.position.y) {
                FindObjectOfType<AudioManager>().Play("SuperNova");
            }
        }
    }
}
