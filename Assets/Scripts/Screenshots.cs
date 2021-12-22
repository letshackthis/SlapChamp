using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshots : MonoBehaviour
{
    private int index = 0;
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ScreenCapture.CaptureScreenshot("SomeLeve" + index.ToString() + ".png");
                index++;
            }
    }
}
