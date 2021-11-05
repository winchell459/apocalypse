using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text Score;
    private int scorenum;
    void updateScore()
    {
        GameObject Deer = GameObject.Find("Deer");
        GetComponent<Timer>().timertext = Score;
        GetComponent<Timer>().time = scorenum;

        if (scorenum > PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore",scorenum);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
