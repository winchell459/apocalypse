using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHandler : MonoBehaviour
{
    static ScreenHandler singleton;
    // Start is called before the first frame update
    void Start()
    {
        if(!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggelFullScreenButton();
        }
    }

    public void ToggelFullScreenButton()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
