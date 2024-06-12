using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGroundMovement : MonoBehaviour
{
    public float currentSpeed;
    Vector3 velocity;

    public bool IsRunning = false;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float maxRunSpeed;
    [SerializeField] public float runAcceleration;
    [SerializeField] float doubleTapTime = 0.3f;
    float lastPressTime;


    [SerializeField] float jumpForce;
    [SerializeField] float gravityForce = -10;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] KeyCode runInput = KeyCode.LeftShift;

    CharacterController controller;
    //[SerializeField] GameObject canvas;
    public bool InLight;
    PlayerControls controls;
    Vector2 movement;
    public float smoothTime = 0.1f;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;
        controls.Player.Jump.performed += ctx => Jumping();
        controls.Player.Run.performed += ctx => IsRunning = true;
        controls.Player.Run.canceled += ctx => IsRunning = false;
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
        controller = GetComponent<CharacterController>();
        //canvas.gameObject.SetActive(true);
        currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Running();
        Move();
        SimulateGravity();
    }


    void Move()
    {
        Vector3 movementInput = transform.right * movement.x + transform.forward * movement.y;
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
        if (IsRunning && IsGrounded())
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxRunSpeed, runAcceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, runAcceleration * Time.deltaTime);
        }
    }

    void Jumping()
    {
        if (IsGrounded())
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
        return Physics.CheckSphere(groundCheck.position, 0.45f, ~playerLayer);
    }
}
