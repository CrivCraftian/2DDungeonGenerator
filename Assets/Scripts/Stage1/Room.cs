using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int roomX;
    public int roomY;

    public int roomID;

    public Vector2 roomPosition { get; set; }

    public List<GridCell> roomGrid;
    public List<GridCell> cornerCells;

    bool isVisited;
    private Dictionary<Room, float> connectedRooms;

    public Room(int rX, int rY, Vector2 roomPos, int RoomID)
    { 
        roomX = rX; 
        roomY = rY;

        roomPosition = roomPos;

        roomID = RoomID;

        isVisited = false;
        
        roomGrid = new List<GridCell>();
        cornerCells = new List<GridCell>();

        connectedRooms = new Dictionary<Room, float>();
    }

    public Vector2 RoomCentre { get { return new Vector2(roomPosition.x + roomX/2, roomPosition.y + roomY/2); } }

    public bool GetIsVisited()
    {
        return isVisited;
    }

    public void SetIsVisited(bool value)
    {
        isVisited = value;
    }

    public Dictionary<Room, float> GetConnections()
    {
        return connectedRooms;
    }

    public void AddConnection(Room addedRoom, float distance)
    {
        if(connectedRooms.ContainsKey(addedRoom)!=true)
        {
            connectedRooms.Add(addedRoom, distance);
            Debug.Log("Worked");
        }
    }

    public void RemoveConnection(Room removedRoom)
    {
        if(connectedRooms.ContainsKey(removedRoom))
        {
            connectedRooms.Remove(removedRoom);
        }
    }
}
