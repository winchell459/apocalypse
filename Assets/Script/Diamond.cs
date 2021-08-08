using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    // Start is called before the first frame update
    {
        if(other.CompareTag("Player"))
        {
            Text scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
            int score = int.Parse(scoreText.text);
            score+=1;
            scoreText.text = score.ToString();

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
