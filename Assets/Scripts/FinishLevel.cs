using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = (Stats)GameObject.Find("Stats").GetComponent("Stats");
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (stats) stats.PrintStats();
            SceneManager.LoadScene("hub");
        }
    }
}
