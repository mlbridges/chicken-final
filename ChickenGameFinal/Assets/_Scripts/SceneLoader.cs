using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ControlsButton()
    {
        SceneManager.LoadScene(3);
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitButton1()
    {
        SceneManager.LoadScene(5);
    }

    public void QuitButton2()
    {
        SceneManager.LoadScene(6);
    }

    public void QuitButton3()
    {
        SceneManager.LoadScene(7);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitForReal()
    {
        Application.Quit();
    }
}
