using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text VolumeText;
    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        DisplayVolume();
    }

    public void VolumeUpButton()
    {
        volumeButton(5);
    }
    public void VolumeDownButton()
    {
        volumeButton(-5);
    }

    private void volumeButton(int step)
    {
        SetMasterVolume(GetMasterVolume() + step);
        DisplayVolume();
    }

    private void DisplayVolume()
    {
        int volume = GetMasterVolume();
        VolumeText.text = $"Volume:\n{volume}";
    }

    public static int GetMasterVolume()
    {
        return PlayerPrefs.GetInt("MasterVolume", 50); 
    }

    public static void SetMasterVolume(int value)
    {
        value = Mathf.Clamp(value, 0, 100);
        PlayerPrefs.SetInt("MasterVolume", value);
    }
}
