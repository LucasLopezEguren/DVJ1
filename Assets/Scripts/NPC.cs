using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public GameObject manageSkillsMessage;

    public GameObject x1;

    public GameObject x2;

    public GameObject SkillTreeUI;

    public GameObject pauseMenu;

    private GameObject player;

    private bool SkillTreeUIIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.x > x1.transform.position.x && player.transform.position.x < x2.transform.position.x) && !SkillTreeUIIsActive && !pauseMenu.GetComponent<PauseMenu>().GameIsPaused)
        {
            manageSkillsMessage.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pauseMenu.SetActive(false);
                SkillTreeUIIsActive = true;
                manageSkillsMessage.SetActive(false);
                SkillTreeUI.SetActive(true);
                Time.timeScale = 0f;
            }
        }
        else
        {
            manageSkillsMessage.SetActive(false);
        }
        if (SkillTreeUIIsActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) BackFromSkilltree();
        }
    }

    public void BackFromSkilltree()
    {
        Time.timeScale = 1f;
        SkillTreeUIIsActive = false;
        SkillTreeUI.SetActive(false);
        pauseMenu.SetActive(true);
    }

}
