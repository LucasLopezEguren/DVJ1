using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    private SkillTree skillTree;

    public GameObject skillPoints;

    [Header("Skills bought")]
    public GameObject shieldBought;
    public GameObject rageBought;
    public GameObject hittingDashBought;
    public GameObject doubleJumppBought;
    public GameObject superBulletBought;
    public GameObject grenadeBought;
    public GameObject laserBought;

    [Header("Skills locked")]
    public GameObject rageLocked;
    public GameObject hittingDashLocked;
    public GameObject grenadeLocked;
    public GameObject laserLocked;

    [Header("Skills not bought")]
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
        CheckBoughtSkill();        
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

    private void CheckBoughtSkill()
    {
        if (!skillTree.skills.shield)
        {
            shieldButton.SetActive(true);
            rageLocked.SetActive(true);       
        }    
        else
        {
            shieldButton.SetActive(false);
            shieldBought.SetActive(true);
            rageLocked.SetActive(false);
        }
        if (!skillTree.skills.rage)
        {
            if (skillTree.skills.shield)
            {
                rageButton.SetActive(true);
            }
            hittingDashLocked.SetActive(true);
        }
        else
        {
            rageButton.SetActive(false);
            rageBought.SetActive(true);
            hittingDashLocked.SetActive(false);
        }
        if (!skillTree.skills.hittingDash)
        {
            hittingDashButton.SetActive(true);
        }
        else
        {
            hittingDashBought.SetActive(true);
        }
        if (!skillTree.skills.doubleJump)
        {
            doubleJumpButton.SetActive(true);
        }
        else
        {
            doubleJumpButton.SetActive(false);
            doubleJumppBought.SetActive(true);
        }
        if (!skillTree.skills.superBullets)
        {
            superBulletsdButton.SetActive(true);
            grenadeLocked.SetActive(true);
        }
        else
        {
            superBulletsdButton.SetActive(false);
            superBulletBought.SetActive(true);
            grenadeLocked.SetActive(false);
        }
        if (!skillTree.skills.grenade)
        {
            if (skillTree.skills.superBullets)
            {
                grenadeButton.SetActive(true);
            }
            laserLocked.SetActive(true);
        }
        else
        {
            grenadeButton.SetActive(false);
            grenadeBought.SetActive(true);
            laserLocked.SetActive(false);
        }
        if (!skillTree.skills.laser)
        {
            laserButton.SetActive(true);
        }
        else
        {
            laserBought.SetActive(true);
        }
    }
}
