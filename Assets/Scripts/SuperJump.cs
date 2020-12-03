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
    private PlayerController playerController;
    void Start() {
        playerController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();
        if ( jumpForce != null) {
                jumpForce = playerController.jumpForce;
                moveSpeed = playerController.moveSpeed;
        }
    }

    private bool addSpeed;
    void FixedUpdate() {
        if (addSpeed) {
            if (playerController.jumpForce < 35) {
                playerController.jumpForce += 0.5f;
            }
            if (playerController.moveSpeed <= 12) {
                playerController.moveSpeed += 0.5f;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){            
            playerController = other.gameObject.GetComponent<PlayerController>();
            playerCurrentHp = playerController.currentHealth;
            enterX = other.transform.position.x;
            enterY = other.transform.position.y;
            addSpeed = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player"){            
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.jumpForce = jumpForce;
            playerController.moveSpeed = moveSpeed;
            addSpeed = false;
            if (playerController.currentHealth >= playerCurrentHp && enterX < other.transform.position.x && enterY < other.transform.position.y) {
                FindObjectOfType<AudioManager>().Play("SuperNova");
            }
        }
    }
}
