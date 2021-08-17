using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0,2,-10);

    // Start is called before the first frame update
    void Start()
    {
        //Offset = Target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target) transform.position = Target.position - Offset;
    }

    public void Setup(Transform target)
    {
        Target = target;
    }
}
