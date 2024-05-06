using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundLook : MonoBehaviour
{
    static float sensitivity = 300;
    Transform player;
    float xRotation;
    float xMouse;
    float yMouse;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GetComponentInParent<PlayerGroundMovement>().transform;
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        xMouse = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        yMouse = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xRotation -= yMouse;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.Rotate(Vector3.up * xMouse);
    }
}
