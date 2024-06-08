using UnityEngine;

public class PrintRenderingCamera : MonoBehaviour
{
    void Update()
    {
        // Check if the "B" key is pressed
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Find the main camera or any other active camera
            Camera currentCamera = Camera.main;

            // If there's no main camera, find any camera (for example, in a multi-camera setup)
            if (currentCamera == null && Camera.allCamerasCount > 0)
            {
                currentCamera = Camera.allCameras[0];
            }

            // Print the camera details if a camera is found
            if (currentCamera != null)
            {
                Debug.Log("Current Rendering Camera: " + currentCamera.name);
            }
            else
            {
                Debug.Log("No active camera found.");
            }
        }
    }
}
