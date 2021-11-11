using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningSheet : MonoBehaviour
{
    public void LoadNext()
    {
        SceneManager.LoadScene("Warning Sheet2");
    }
    public void LoadEnd()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
