using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void Level1()
    {
        LoadLevel1();
    }
    public void Level2()
    {
        LoadLevel2();
    }
    public void Level3()
    {
        LoadLevel3();
    }
    void LoadLevel1()
    {
        SceneManager.LoadScene("map 1");
    }
    void LoadLevel2()
    {
        SceneManager.LoadScene("map 2");
    }
    void LoadLevel3()
    {
        SceneManager.LoadScene("map 3");
    }
}