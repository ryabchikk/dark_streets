using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UIAnimations;
public class ButtonMethods : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void OpenPause(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void Resume(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
