using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 10;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        //rb.AddForce(Speed * new Vector3(horizontal,0,vertical));
        rb.velocity=(Speed * new Vector3(horizontal,0,vertical) + new Vector3(0,rb.velocity.y,0));
    }
}
