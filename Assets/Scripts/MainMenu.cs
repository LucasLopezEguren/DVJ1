using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PauseMenu PauseMenu;

    public LevelLoader levelLoader;

    public void PlayGame()
    {
        levelLoader.LoadNextLevel("Hub");
    }

    public void ResumeGame()
    {
        PauseMenu.Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Feedback()
    {
        Application.OpenURL("https://forms.gle/3o1fGK1hvSGvCsNL8");
    }

}
