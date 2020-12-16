using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    private SkillTree skillTree;

    public GameObject skillPoints;

    [Header("Skills buttons")]
    public GameObject shieldButton;
    public GameObject rageButton;
    public GameObject hittingDashButton;
    public GameObject doubleJumpButton;
    public GameObject superBulletsdButton;
    public GameObject grenadeButton;
    public GameObject laserButton;

    // Start is called before the first frame update
    void Start()
    {
        skillTree = (SkillTree)GameObject.Find("SkillTree").GetComponent("SkillTree");
        skillPoints.GetComponent<TMPro.TextMeshProUGUI>().text = skillTree.skillsPointToSpend.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        skillPoints.GetComponent<TMPro.TextMeshProUGUI>().text = skillTree.skillsPointToSpend.ToString();
        if (!skillTree.skills.shield)
        {
            rageButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            rageButton.GetComponent<Button>().interactable = true;
        }
        if (!skillTree.skills.rage)
        {
            hittingDashButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            hittingDashButton.GetComponent<Button>().interactable = true;
        }
        if (!skillTree.skills.superBullets)
        {
            grenadeButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            grenadeButton.GetComponent<Button>().interactable = true;
        }
        if (!skillTree.skills.grenade)
        {
            laserButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            laserButton.GetComponent<Button>().interactable = true;
        }
    }

    public void GetShieldSkill()
    {
        if (skillTree.skillsPointToSpend > 0)
        {
            skillTree.skills.shield = true;
            skillTree.skillsPointToSpend--;
        }
    }

    public void GetRageSkill()
    {
        if (skillTree.skillsPointToSpend > 0)
        {
            skillTree.skills.rage = true;
            skillTree.skillsPointToSpend--;

        }
    }

    public void GetHittingDashSkill()
    {
        if (skillTree.skillsPointToSpend > 0)
        {
            skillTree.skills.hittingDash = true;
            skillTree.skillsPointToSpend--;
        }
    }

    public void GetDoubleJumpSkill()
    {
        if (skillTree.skillsPointToSpend > 0)
        {
            skillTree.skills.doubleJump = true;
            skillTree.skillsPointToSpend--;
        }
    }

    public void GetSuperBulletsSkill()
    {
        if (skillTree.skillsPointToSpend > 0)
        {
            skillTree.skills.superBullets = true;
            skillTree.skillsPointToSpend--;
        }
    }

    public void GetGrenadeSkill()
    {
        if (skillTree.skillsPointToSpend > 0)
        {
            skillTree.skills.grenade = true;
            skillTree.skillsPointToSpend--;
        }
    }

    public void GetLaserSkill()
    {
        if (skillTree.skillsPointToSpend > 0)
        {
            skillTree.skills.laser = true;
            skillTree.skillsPointToSpend--;
        }
    }
}
