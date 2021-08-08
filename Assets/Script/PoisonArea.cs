using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    public float poisonRate = 0.2f;
    private float shrinkRate = 0.3f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<health>().Poison(poisonRate);
        }
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<health>().Poison(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }
    private void Update()
    {
        GetComponent<SphereCollider>().radius -= shrinkRate * Time.deltaTime;
    }
}
