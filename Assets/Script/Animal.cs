using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0, -5, 10);
    private Animator anim;
    public bool Eating;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target)
        {
            transform.position = Target.position - Offset;
            Vector3 targetVel = Target.GetComponent<Rigidbody>().velocity;
            if (targetVel.magnitude > 0.1f) transform.right = new Vector3(targetVel.x, 0, targetVel.z);
        }
        anim.SetBool("Eating", Eating);
    }
}
