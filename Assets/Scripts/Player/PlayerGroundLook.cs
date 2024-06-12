using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundLook : MonoBehaviour
{
    public PlayerControls controls;
    private Transform playerBody;
    public float mouseSensitivity = 100f;
    public float gamepadSensitivity = 100f;
    private Vector2 lookInput;
    private float xRotation = 0f;
    private Transform camTransform;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        controls.Player.Look.canceled += ctx => Look(Vector2.zero);
        playerBody = GameObject.FindGameObjectWithTag("Player").transform;
        camTransform = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Update()
    {
        LookInput();
    }

    void Look(Vector2 input)
    {
        lookInput = input;
    }

    void LookInput()
    {
        if (lookInput == Vector2.zero) return;

        float sensitivity = GetCurrentSensitivity();

        float mouseX = lookInput.x * sensitivity * Time.deltaTime;
        float mouseY = lookInput.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    float GetCurrentSensitivity()
    {
        if (Gamepad.current != null && Gamepad.current.enabled)
        {
            return gamepadSensitivity;
        }
        return mouseSensitivity;
    }
}
