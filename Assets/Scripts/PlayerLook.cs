using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 300;
    [SerializeField] float spinSpeed = 10;

    [SerializeField] Transform playerBody;

    public float mouseX;
    public float mouseY;
    public float zRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q))
        {
            zRotation = 1 * spinSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            zRotation = -1 * spinSpeed * Time.deltaTime;
        }
        else
        {
            zRotation = 0;
        }

        transform.Rotate(-mouseY, mouseX, zRotation);
    }
}
