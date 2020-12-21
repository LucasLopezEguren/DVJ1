using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private Stats stats;

    private SkillTree skillTree;

    private bool expAdded = false;

    public string SceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        stats = (Stats)GameObject.Find("Stats").GetComponent("Stats");
        skillTree = (SkillTree)GameObject.Find("SkillTree").GetComponent("SkillTree");
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (stats) stats.LevelComplete = true;
            if (skillTree && !expAdded)
            {
                skillTree.LevelComplete();
                expAdded = true;
            }
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
