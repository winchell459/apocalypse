using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartPlay()
    {
        LoadStartPlay();
    }
    public void Settings()
    {
        LoadSettings();
    }
    public void HelpAdvice()
    {
        LoadHelpAdvice();
    }
    void LoadStartPlay()
    {
        SceneManager.LoadScene("level");
    }
    void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    void LoadHelpAdvice()
    {
        SceneManager.LoadScene("HelpAdvice");
    }
}
