using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 10;
    private Rigidbody rb;

    public Transform CameraRig;

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
        Vector3 MoveHorizontally = Speed * horizontal * CameraRig.right;
        Vector3 MoveForward = Speed * vertical * CameraRig.forward;
        Vector3 MoveVertically = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = MoveHorizontally + MoveForward + MoveVertically;

    }
}
