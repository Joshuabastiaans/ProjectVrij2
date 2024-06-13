using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 200;
    [SerializeField] float spinSpeed = 10;

    [SerializeField] Transform playerBody;

    private PlayerControls controls;
    private Vector2 lookInput;
    public float zRotation;

    [SerializeField] bool alwaysRotating;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
        controls.Enable();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q))
        {
            zRotation = 1 * spinSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E) || alwaysRotating)
        {
            zRotation = -1 * spinSpeed * Time.deltaTime;
        }
        else
        {
            zRotation = 0;
        }

        // Apply rotation
        transform.Rotate(-mouseY, mouseX, zRotation);
    }
}
