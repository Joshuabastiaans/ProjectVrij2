using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour
{
    public bool IsLocked = false;
    [HideInInspector] public bool canInteract = false;
    [HideInInspector] public bool isOpen = false;
    private Animation doorAnimation;

    [SerializeField] private bool isLeftDoor = false;
    [SerializeField] private bool isRightDoor = false;
    private Vector3 doorPosition = new Vector3(-2.56f, 0, 23.10f);
    PlayerControls controls;
    DoorManager doorManager;
    public bool createRoom = true;
    [HideInInspector] public bool createExit = false;
    bool createdExitRoom = false;
    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Interact.performed += ctx =>
        {
            if (canInteract && !IsLocked)
            {
                print(isOpen);
                if (isOpen)
                {
                    CloseDoor();
                }
                else
                {
                    OpenDoor();
                }
            }
        };
        if (isLeftDoor)
        {
            doorPosition = new Vector3(-2.56f, 0, 23.10f);
        }
        else if (isRightDoor)
        {
            doorPosition = new Vector3(2.56f, 0, 23.10f);
        }
        if (createRoom)
        {
            doorManager = transform.parent.parent.GetComponent<DoorManager>();
        }
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
        doorAnimation = GetComponent<Animation>();
    }

    public void OpenDoor()
    {
        if (createdExitRoom)
        {
            return;
        }
        doorAnimation.Play("DoorOpen");
        if (createExit)
        {
            doorManager.SetExit();
            doorManager.CreateExit(new Vector3(0f, 0f, -12.6f));
            createdExitRoom = true;
            print("Exit Created");
            return;
        }

        if (createRoom)
            doorManager.CreateRoom(doorPosition, gameObject);
        isOpen = true;

    }

    public void CloseDoor()
    {
        if (createdExitRoom)
        {
            return;
        }
        isOpen = false;
        doorAnimation.Play("DoorClose");
    }
}
