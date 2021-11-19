using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void Start()
    {
        audioSource.volume = ((float)SettingsHandler.GetMasterVolume()) / 100;
    }
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
