using UnityEngine;

public class SetDoorInteract : MonoBehaviour
{

    DoorController doorController;

    void Start()
    {
        doorController = GetComponentInChildren<DoorController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorController.canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorController.canInteract = false;
        }
    }
}
