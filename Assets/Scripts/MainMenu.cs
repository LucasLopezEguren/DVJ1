using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PauseMenu PauseMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene("Hub");
    }

    public void ResumeGame()
    {
        PauseMenu.Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
