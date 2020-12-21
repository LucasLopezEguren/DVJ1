using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStats : MonoBehaviour
{
    public GameObject timeSeg;

    public GameObject timeMin;

    public GameObject enemyKilled;

    public GameObject maxCombo;

    public GameObject score;

    public GameObject skillPointsToSpend;

    private Stats stats;

    private SkillTree skillTree;

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
        timeSeg.GetComponent<Text>().text = secondsToShow;
        timeMin.GetComponent<Text>().text = minutes.ToString();
        //time.GetComponent<Text>().text = stats.TimeToCompleteLevel.ToString();
        enemyKilled.GetComponent<Text>().text = stats.EnemyKilled.ToString();
        maxCombo.GetComponent<Text>().text = stats.MaxCombo.ToString();
        score.GetComponent<Text>().text = stats.Score.ToString();
        if(skillTree.skills.HasAllSkills()) skillPointsToSpend.SetActive(false);
        else
        {
            skillPointsToSpend.GetComponent<Text>().text = skillTree.skillsPointEarnedInLevel.ToString();           
        }           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}