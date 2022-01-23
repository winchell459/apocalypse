using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0,-5,10);
    public Transform CameraPitch;

    private Vector2 mousePos;

    public float RotateScale = 1;
    public float PitchScale = 1;

    public float min = 1, max = 60;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Offset = Target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target)
        {
            transform.position = Target.position - Offset;
            float dx = Input.GetAxis("Mouse X");
            float dy = Input.GetAxis("Mouse Y");

            float PitchAngle = CameraPitch.eulerAngles.x + dy * PitchScale;

            PitchAngle = Mathf.Clamp(PitchAngle, min, max);

            CameraPitch.eulerAngles = new Vector3(PitchAngle, CameraPitch.eulerAngles.y, CameraPitch.eulerAngles.z);

            transform.eulerAngles += new Vector3(0, dx * RotateScale, 0);

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void Setup(Transform target)
    {
        Target = target;
    }

}
