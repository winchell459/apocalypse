using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text Score;
    
    public static void UpdateScore(int scorenum)
    {
        if (scorenum > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", scorenum);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("High score is on" + gameObject.name);

        int scorenum = PlayerPrefs.GetInt("HighScore", 0);
        Score.text = Timer.FormatTime(scorenum);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
   
}
