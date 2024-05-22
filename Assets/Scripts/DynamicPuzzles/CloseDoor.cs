using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{

    DoorController doorController;
    bool hasBeenClosed = false;

    void Start()
    {
        doorController = transform.parent.GetComponentInChildren<DoorController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenClosed)
        {
            doorController.CloseDoor();
            hasBeenClosed = true;
            doorController.createExit = true;
            doorController.isOpen = false;
        }
    }
}
