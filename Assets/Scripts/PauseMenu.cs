using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;

    public GameObject ControlsMenuUI;

    public GameObject SettingsMenuUI;

    public static bool GameIsPaused = false;

    public GameObject CanvasUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }            
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);
        ControlsMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        GameIsPaused = false;
        if (CanvasUI)
        {
            CanvasUI.SetActive(true);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PauseMenuUI.SetActive(true);
        GameIsPaused = true;
        if (CanvasUI)
        {
            CanvasUI.SetActive(false);
        }
    }
}
