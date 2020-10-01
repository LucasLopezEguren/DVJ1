using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    public GameObject player;
    void Start () {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            SceneManager.LoadScene("hub");
        } else {
            Destroy(other.gameObject);
        }
    }
}
