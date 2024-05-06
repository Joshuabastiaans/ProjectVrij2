using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundMovement : MonoBehaviour
{
    public float currentSpeed;
    Vector3 velocity;
    float xInput;
    float zInput;

    public bool IsRunning = false;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float maxRunSpeed;
    [SerializeField] public float runAcceleration;
    [SerializeField] float doubleTapTime = 0.3f;
    float lastPressTime;


    [SerializeField] float jumpForce;
    [SerializeField] float gravityForce = -10;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] KeyCode runInput = KeyCode.LeftShift;

    CharacterController controller;

    public bool InLight;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Running();
        Jumping();
        SimulateGravity();
    }

    void Movement()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        Vector3 movementInput = transform.right * xInput + transform.forward * zInput;
        if (movementInput.x > 1 || movementInput.z > 1) // Normalize movement only when it's more than 1
        {
            controller.Move(movementInput.normalized * currentSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(movementInput * currentSpeed * Time.deltaTime);
        }
    }

    void Running()
    {
        if (IsRunning)
        {
            currentSpeed += runAcceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        if (currentSpeed >= maxRunSpeed)
        {
            currentSpeed = maxRunSpeed;
        }
        if (currentSpeed <= moveSpeed)
        {
            currentSpeed = moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            float timeSinceLastPress = Time.time - lastPressTime;
            if (timeSinceLastPress <= doubleTapTime)
            {
                IsRunning = true;
            }
            else
            {
                IsRunning = false;
            }
            lastPressTime = Time.time;
        }

        if (!Input.GetKey(KeyCode.W))
        {
            IsRunning = false;
        }

        if (Input.GetKey(runInput))
        {
            IsRunning = true;
        }
        else
        {
            IsRunning = false;
        }

    }

    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravityForce);
        }
    }

    void SimulateGravity()
    {
        velocity.y += gravityForce * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2;
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
    }
}
