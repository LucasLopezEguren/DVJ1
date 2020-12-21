using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{

    public int skillsPointToSpend = 0;

    public int skiilsPointsThatHave = 0;

    public int skiilsPointsEver = 0;

    public int skillsPointEarnedInLevel = 0;

    public int totalExperienceEarned = 0;

    public int experienceEarnedInLevel = 0;

    public int experienceToLevel = 70;   

    public static SkillTree instance;

    public Skills skills;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelComplete()
    {
        skiilsPointsThatHave = skillsPointToSpend;
        skillsPointEarnedInLevel = experienceEarnedInLevel / experienceToLevel;
        skiilsPointsEver += skillsPointEarnedInLevel;        
        skillsPointToSpend += experienceEarnedInLevel / experienceToLevel;
        if(skiilsPointsEver >= 7)
        {
            skillsPointToSpend = 7 - skills.SkilsActivated();
        }
        totalExperienceEarned += experienceEarnedInLevel;
    }

    public void RestExperiencedEarnedInLevel()
    {
        experienceEarnedInLevel = 0;
    }

    public void AddExperience(int exp)
    {
        experienceEarnedInLevel += exp;
    }
}
