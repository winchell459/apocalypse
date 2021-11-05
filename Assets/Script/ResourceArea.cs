using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceArea : MonoBehaviour
{
    public float MaxResource = 20;
    public float CurrentResource = 20;
    public float stepTime = 5;
    private float stepTimeStart;
    public float stepAmount = 5;
    private bool PlayerEating;
    public float RefillRate = 0.1f;



    private void Update()
    {
        if (!PlayerEating)
        {
            CurrentResource = Mathf.Clamp(CurrentResource + RefillRate * Time.deltaTime, 0, MaxResource);
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) stepTimeStart = Time.fixedTime;
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.fixedTime > stepTimeStart + stepTime)
            {
                float addAmount = Mathf.Clamp(stepAmount, 0, CurrentResource);
                CurrentResource -= addAmount;
                FindObjectOfType<health>().AddHealth(addAmount);

                stepTimeStart = Time.fixedTime;
            }
            FindObjectOfType<Animal>().Eating = true;
            FindObjectOfType<health>().isEating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerEating = false;
            FindObjectOfType<Animal>().Eating = false;
            FindObjectOfType<health>().isEating = false;
        }
    }

    public void AddHealth(float value)
    {
        CurrentResource = Mathf.Clamp(CurrentResource + stepAmount, 0, MaxResource);
    }
}
