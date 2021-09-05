using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0,2,-10);
    public Transform CameraPitch;

    

    public float RotateScale = 1;
    public float PitchScale = 1;

    public float min = 1, max = 30;
    // Start is called before the first frame update
    void Start()
    {
        //Offset = Target.position - transform.position;
        
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target)
        {
            transform.position = Target.position - Offset;

            float dx = Input.GetAxis("Mouse X");
            float dy = Input.GetAxis("Mouse Y");

            float pitchAngle = CameraPitch.eulerAngles.x + dy * PitchScale;
            pitchAngle = Mathf.Clamp(pitchAngle, min, max);

            CameraPitch.eulerAngles = new Vector3(pitchAngle, CameraPitch.eulerAngles.y, CameraPitch.eulerAngles.z);

            //check to see if CameraPitch.eulerAngles.x > max or < min
            //CameraPitch.eulerAngles = new Vector3(max, CameraPitch.eulerAngles.y, CameraPitch.eulerAngles.z);

            transform.eulerAngles += new Vector3(0, dx * RotateScale, 0);

        }
    }

    public void Setup(Transform target)
    {
        Target = target;
    }
}
