using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0,2,-10);
    public Transform CameraPitch;

    private Vector2 mousePos;

    public float RotateScale = 1;
    public float PitchScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Offset = Target.position - transform.position;
        mousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target) transform.position = Target.position - Offset;

        float dx = Input.mousePosition.x - mousePos.x;
        float dy = Input.mousePosition.y - mousePos.y;
        CameraPitch.eulerAngles += new Vector3(dy * PitchScale, 0, 0);
        //check to see if CameraPitch.eulerAngles.x > max or < min
        //CameraPitch.eulerAngles = new Vector3(max, CameraPitch.eulerAngles.y, CameraPitch.eulerAngles.z);

        transform.eulerAngles += new Vector3(0, dx * RotateScale, 0);

        mousePos = Input.mousePosition;
    }

    public void Setup(Transform target)
    {
        Target = target;
    }
}
