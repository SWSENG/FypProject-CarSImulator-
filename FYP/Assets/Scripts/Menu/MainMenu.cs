using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void starGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void backButton()
    {
        SceneManager.LoadScene(0);
    }
    public void tutorialScene()
    {
        SceneManager.LoadScene(2);
    }
    public void skillMode()
    {
        SceneManager.LoadScene(3);
    }
    public void freeMode()
    {
        SceneManager.LoadScene(4);
    }

    public void credit()
    {
        SceneManager.LoadScene(5);
    }
}
