using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform Target;
    private Vector3 Offset;
    // Start is called before the first frame update
    void Start()
    {
        Offset = Target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Target.position - Offset;
    }
}
