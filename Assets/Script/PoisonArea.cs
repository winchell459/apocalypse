using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    public float poisonRate = 0.2f;
    [SerializeField] private float startRadius;
    [SerializeField] private float shrinkRate = 0.1f;
    [SerializeField] private ParticleSystem cloud;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<health>().Poison(poisonRate);
        }
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<health>().Poison(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }
    private void Update()
    {
        GetComponent<SphereCollider>().radius -= shrinkRate * Time.deltaTime;
        GetComponent<SphereCollider>().radius = Mathf.Clamp(GetComponent<SphereCollider>().radius, 0, float.MaxValue);
        ParticleSystem.SizeOverLifetimeModule sz = cloud.sizeOverLifetime;

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(GetComponent<SphereCollider>().radius / startRadius, 0.0f);
        curve.AddKey(1.0f, 1.0f);
        
        sz.size = new ParticleSystem.MinMaxCurve(1, curve);
    }
    public void Setup(Vector3 pos, float radius)
    {
        startRadius = radius;
        transform.position = pos;
        GetComponent<SphereCollider>().radius = radius;
        cloud.gameObject.transform.localScale = new Vector3(radius / 20, radius / 20, radius / 20);
    }
}
