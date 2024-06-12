using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public GameObject roomPrefab;
    public GameObject exitPrefab;
    private int roomIDCounter = 0;
    private Dictionary<int, GameObject> roomDictionary = new Dictionary<int, GameObject>();
    public int currentRoomID = 0;
    [HideInInspector] public bool createExit = false;
    public GameObject firstRoom;
    [HideInInspector] public bool isLeftDoor = false;
    [HideInInspector] public bool isRightDoor = false;

    private GameObject currentRoom;
    public void GenerateRoom(Vector3 relativePosition)
    {

        GameObject room = Instantiate(roomPrefab, GetLastRoomPosition() + relativePosition, Quaternion.identity);
        roomIDCounter++;
        room.name = "Room_" + roomIDCounter;
        roomDictionary.Add(roomIDCounter, room);
    }

    public void GenerateExit(Vector3 relativePosition)
    {
        Instantiate(exitPrefab, GetLastRoomPosition() + relativePosition, Quaternion.identity);
    }

    public void DisableRooms()
    {
        print("Disabling rooms" + currentRoomID);

        if (currentRoomID != 0)
        {
            firstRoom.SetActive(false);
            if (isLeftDoor)
            {
                firstRoom.GetComponent<Animation>().Play("RightDoorFuck");
            }
            else if (isRightDoor)
            {
                firstRoom.GetComponent<Animation>().Play("LeftDoorFuck");
            }
        }
        foreach (var roomID in roomDictionary.Keys)
        {
            if (roomID < currentRoomID)
            {
                roomDictionary[roomID].SetActive(false);
            }
        }
        currentRoom = roomDictionary[currentRoomID];
        if (isLeftDoor)
        {
            currentRoom.GetComponent<Animation>().Play("RightDoorFuck");
        }
        else if (isRightDoor)
        {
            currentRoom.GetComponent<Animation>().Play("LeftDoorFuck");
        }


    }

    public void DestroyRoom(int roomID)
    {
        if (roomDictionary.ContainsKey(roomID))
        {
            GameObject room = roomDictionary[roomID];
            roomDictionary.Remove(roomID);
            Destroy(room);
            roomIDCounter--;
        }
        else
        {
            Debug.LogWarning("Room ID " + roomID + " does not exist.");
        }
    }

    public void DestroyLastRoom()
    {
        if (roomDictionary.Count > 0)
        {
            int lastRoomID = -1;
            foreach (var roomID in roomDictionary.Keys)
            {
                lastRoomID = roomID;
            }

            DestroyRoom(lastRoomID);
        }
    }

    public Vector3 GetLastRoomPosition()
    {
        if (roomDictionary.ContainsKey(roomIDCounter))
        {
            return roomDictionary[roomIDCounter].transform.position;
        }
        else
        {
            Debug.LogWarning("No rooms have been created yet.");
            return transform.position;
        }
    }
}
