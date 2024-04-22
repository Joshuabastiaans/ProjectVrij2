using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;

    CharacterController controller;

    float movementX;
    float movementZ;
    float movementY;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.Space))
        {
            movementY = 1;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            movementY = -1;
        }
        else
        {
            movementY = 0;
        }

        Vector3 move = transform.right * movementX + transform.forward * movementZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity = movementY * transform.up;
        controller.Move(velocity * moveSpeed * Time.deltaTime);
    }
}
