using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStats : MonoBehaviour
{
    private Stats stats;

    private SkillTree skillTree;

    public GameObject time;

    public GameObject enemyKilled;

    public GameObject maxCombo;

    public GameObject score;

    public GameObject experienceEarned;

    public GameObject level;

    public GameObject skillPointsToSpend;

    // Start is called before the first frame update
    void Start()
    {
        stats = (Stats)GameObject.Find("Stats").GetComponent("Stats");
        skillTree = (SkillTree)GameObject.Find("SkillTree").GetComponent("SkillTree");
        float minutes = Mathf.Floor(stats.TimeToCompleteLevel / 60);
        float seconds = stats.TimeToCompleteLevel % 60;
        string secondsToShow;
        if(seconds < 10)
        {
            secondsToShow = "0" + Mathf.RoundToInt(seconds).ToString();
        }
        else
        {
            secondsToShow = Mathf.RoundToInt(seconds).ToString();
        }
        time.GetComponent<TMPro.TextMeshProUGUI>().text = minutes.ToString() + ":" + secondsToShow;
        enemyKilled.GetComponent<TMPro.TextMeshProUGUI>().text = stats.EnemyKilled.ToString();
        maxCombo.GetComponent<TMPro.TextMeshProUGUI>().text = stats.MaxCombo.ToString();
        score.GetComponent<TMPro.TextMeshProUGUI>().text = stats.Score.ToString();
        experienceEarned.GetComponent<TMPro.TextMeshProUGUI>().text = skillTree.experienceEarnedInLevel.ToString();
        level.GetComponent<TMPro.TextMeshProUGUI>().text = skillTree.level.ToString();
        skillPointsToSpend.GetComponent<TMPro.TextMeshProUGUI>().text = skillTree.skillsPointToSpend.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}