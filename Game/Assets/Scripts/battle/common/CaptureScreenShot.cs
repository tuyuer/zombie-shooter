using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureScreenShot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log(Application.persistentDataPath);
            ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/CaptureScreenshot.png");
        }
    }
}
