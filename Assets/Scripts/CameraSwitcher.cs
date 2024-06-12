using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CameraFader cameraFader;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cameraFader.FadeToSecondCamera();
        }
    }
}
