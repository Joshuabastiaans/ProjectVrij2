using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public bool IsLocked = false;
    private Animation doorAnimation;

    PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Interact.performed += ctx => OpenDoor();
    }

    // Start is called before the first frame update
    void Start()
    {
        doorAnimation = GetComponent<Animation>();
    }

    void OpenDoor()
    {
        doorAnimation.Play("DoorOpen");
    }

    void CloseDoor()
    {
        doorAnimation.Play("DoorClose");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!IsLocked)
            {
                OpenDoor();
            }
        }
    }
}
