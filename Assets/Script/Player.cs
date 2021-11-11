using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 10;
    private Rigidbody rb;

    public Transform CameraRig;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            //rb.AddForce(Speed * new Vector3(horizontal,0,vertical));
            Vector3 MoveHorizontally = Speed * horizontal * CameraRig.right;
            Vector3 Moveforward = Speed * vertical * CameraRig.forward;
            rb.velocity = MoveHorizontally + Moveforward + new Vector3(0, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
