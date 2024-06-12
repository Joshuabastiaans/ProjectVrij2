using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{

    DoorController doorController;
    bool hasBeenClosed = false;
    RoomManager roomManager;
    void Start()
    {
        doorController = transform.parent.GetComponentInChildren<DoorController>();
        roomManager = FindObjectOfType<RoomManager>();
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
        if (other.CompareTag("Player"))
        {
            string parentName = doorController.transform.parent.parent.parent.name;
            char lastCharacter = GetLastCharacter(parentName);

            int parentID = int.Parse(lastCharacter.ToString());
            roomManager.currentRoomID = parentID;
            if (doorController.isLeftDoor)
            {
                roomManager.isRightDoor = false;
                roomManager.isLeftDoor = true;
            }
            else if (doorController.isRightDoor)
            {
                roomManager.isLeftDoor = false;
                roomManager.isRightDoor = true;
            }
        }
    }

    char GetLastCharacter(string name)
    {
        return name[name.Length - 1];
    }
}
