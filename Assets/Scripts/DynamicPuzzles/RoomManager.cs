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
    public GameObject kaoloDelete;
    [HideInInspector] public bool isLeftDoor = false;
    [HideInInspector] public bool isRightDoor = false;
    PlayerAudio playerAudio;
    public GameObject kauloWereld;

    private GameObject currentRoom;

    void Start()
    {
        playerAudio = FindObjectOfType<PlayerAudio>();
    }
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
        kaoloDelete.SetActive(false);
        if (currentRoomID != 0)
        {
            firstRoom.SetActive(false);
        }
        foreach (var roomID in roomDictionary.Keys)
        {
            if (roomID < currentRoomID)
            {
                roomDictionary[roomID].SetActive(false);
            }
        }
        currentRoom = roomDictionary[currentRoomID];
        if (currentRoomID == 0)
        {
            if (isLeftDoor)
            {
                print("Left door");
                firstRoom.GetComponent<Animation>().Play("RightDoorFuck");
            }
            else if (isRightDoor)
            {
                print("Right door");
                firstRoom.GetComponent<Animation>().Play("LeftDoorFuck");
            }
        }
        if (isLeftDoor)
        {
            currentRoom.GetComponent<Animation>().Play("RightDoorFuck");
        }
        else if (isRightDoor)
        {
            currentRoom.GetComponent<Animation>().Play("LeftDoorFuck");
        }
        Vector3 roomPosition = roomDictionary[roomIDCounter].transform.position;
        kauloWereld.transform.position = roomPosition + new Vector3(22.79f, 2.07f, -326.91f);
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
