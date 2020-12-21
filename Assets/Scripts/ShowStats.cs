﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStats : MonoBehaviour
{
    public GameObject time;

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
        time.GetComponent<TMPro.TextMeshProUGUI>().text = minutes.ToString() + ":" + secondsToShow;
        enemyKilled.GetComponent<TMPro.TextMeshProUGUI>().text = stats.EnemyKilled.ToString();
        maxCombo.GetComponent<TMPro.TextMeshProUGUI>().text = stats.MaxCombo.ToString();
        score.GetComponent<TMPro.TextMeshProUGUI>().text = stats.Score.ToString();
        if(skillTree.skills.HasAllSkills()) skillPointsToSpend.SetActive(false);
        else
        {
            skillPointsToSpend.GetComponent<TMPro.TextMeshProUGUI>().text = skillTree.skillsPointEarnedInLevel.ToString();           
        }           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}