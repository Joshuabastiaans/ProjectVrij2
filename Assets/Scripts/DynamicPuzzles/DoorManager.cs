using UnityEngine;

public class DoorManager : MonoBehaviour
{

    bool createdRoom = false;
    RoomManager roomManager;
    GameObject m_lastDoorOpened;

    void Awake()
    {
        roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
    }
    public void CreateRoom(Vector3 doorPosition, GameObject lastDoorOpened)
    {
        if (m_lastDoorOpened != null && m_lastDoorOpened != lastDoorOpened)
        {
            m_lastDoorOpened.GetComponent<DoorController>().CloseDoor();
            roomManager.DestroyLastRoom();
        }

        m_lastDoorOpened = lastDoorOpened;
        roomManager.GenerateRoom(doorPosition);

        createdRoom = true;
    }

    public void CreateExit(Vector3 doorPosition)
    {
        roomManager.GenerateExit(doorPosition);

        return;
    }

    public void SetExit()
    {
        roomManager.createExit = true;
    }
}
