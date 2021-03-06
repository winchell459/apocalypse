using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
public class health : MonoBehaviour
{
    private bool cooling, hpstart = true;
    [SerializeField] private float normalRate = 0.1f, poisonRate = 0.2f;
    [SerializeField] private int xmin = 0, xmax = 100;
    private float currentValue = 100;
    public bool isPoisoned = false;

    Text healthText;

    // Use this for initialization
    void Start()
    {

        healthText = GameObject.FindGameObjectWithTag("health").GetComponent<Text>();//获取组件方法

        //currentValue = int.Parse(healthText.text);
    }

    // Update is called once per frame

    void Update()
    {
        //血量逻辑开始
        if (hpstart)
        {
            float rate = isPoisoned ? poisonRate : normalRate;

            currentValue = Mathf.Clamp(currentValue - rate * Time.deltaTime, xmin, xmax);

            healthText.text = "Health: " + currentValue;
        }
    }

    public void Poison(float rate)
    {
        isPoisoned = true;
        poisonRate = rate;
    }

    public void Poison(bool isPoisoned)
    {
        this.isPoisoned = isPoisoned;
    }
    public float GetHealth()
    {
        return currentValue;
    }

    public void AddHealth(float value)
    {
        currentValue = Mathf.Clamp(currentValue + value, xmin, xmax);
    }
}

    
    
