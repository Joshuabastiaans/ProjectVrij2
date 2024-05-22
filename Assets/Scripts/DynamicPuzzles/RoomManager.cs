using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public GameObject roomPrefab;
    public GameObject exitPrefab;
    private int roomIDCounter = 0;
    private Dictionary<int, GameObject> roomDictionary = new Dictionary<int, GameObject>();

    [HideInInspector] public bool createExit = false;
    public void GenerateRoom(Vector3 relativePosition)
    {

        GameObject room = Instantiate(roomPrefab, GetLastRoomPosition() + relativePosition, Quaternion.identity);
        roomIDCounter++;
        room.name = "Room_" + roomIDCounter; // Optional: Name the room for easier debugging
        roomDictionary.Add(roomIDCounter, room);
    }

    public void GenerateExit(Vector3 relativePosition)
    {
        Instantiate(exitPrefab, GetLastRoomPosition() + relativePosition, Quaternion.identity);
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
            return new Vector3(0.62f, -0.845f, -9.326f);
        }
    }
}
