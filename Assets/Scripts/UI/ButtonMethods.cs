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

    public void OpenLeftAnimatePanel(GameObject panel)
    {
        panel.SetActive(true);
        XUIMover(panel,-Screen.width);
    }
    public void OpenFighterMarketAnimatePanel(GameObject panel)
    {
        panel.SetActive(true);
        YUIMover(panel, -Screen.height, 0.5f, LeanTweenType.easeOutBack);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void LoadScene(int numberScene)
    {
        SceneManager.LoadScene(numberScene);
    }

    public void ActivatePause()
    {
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
    }
}
