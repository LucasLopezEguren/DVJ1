using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{

    public LevelLoader levelLoader;

    [SerializeField]private string levelToLoad;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            levelLoader.LoadNextLevel(levelToLoad);
        }
    }
}
