using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsUI : MonoBehaviour
{
    public GameObject shield;

    public GameObject rage;

    public GameObject grenade;

    public GameObject laser;

    [HideInInspector]
    public GameObject selectedSkill;

    private int index;

    private SkillTree skillTree;

    private GameObject[] skillsActivated;

    // Start is called before the first frame update
    void Start()
    {
        skillTree = (SkillTree)GameObject.Find("SkillTree").GetComponent("SkillTree");
        index = 0;
        if(skillTree.skills.SkillsActivatedThatNeedToBeSelected() > 0)
        {
            skillsActivated = new GameObject[skillTree.skills.SkillsActivatedThatNeedToBeSelected()];            
            if (skillTree.skills.shield)
            {
                skillsActivated[index] = shield;
                index++;
            }
            if (skillTree.skills.rage)
            {
                skillsActivated[index] = rage;
                index++;
            }
            if (skillTree.skills.grenade)
            {
                skillsActivated[index] = grenade;
                index++;
            }
            if (skillTree.skills.laser)
            {
                skillsActivated[index] = laser;
                index++;
            }
            index = 0;
            selectedSkill = skillsActivated[index];
            selectedSkill.SetActive(true);
        }             
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSkillSelected();
    }

    private void ChangeSkillSelected()
    {
        if(skillTree.skills.SkillsActivatedThatNeedToBeSelected() > 1 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            index++;
            if (index >= skillsActivated.Length) index = 0;
            selectedSkill.SetActive(false);
            selectedSkill = skillsActivated[index];
            selectedSkill.SetActive(true);
        }
    }
}
