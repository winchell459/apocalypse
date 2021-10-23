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
        }
        anim.SetBool("Eating", Eating);
    }
}
