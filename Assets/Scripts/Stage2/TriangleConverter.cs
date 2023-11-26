using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TriangleConverter
{
    public static void RoomConnecterUsingTriangles(Triangle[] Trilist, Room[] rooms)
    {
        Dictionary<Vector2, Room> roomLocation = new Dictionary<Vector2, Room>();
        foreach(Room room in rooms)
        {
            roomLocation.Add(room.roomPosition, room);
        }


        foreach (Triangle triangle in Trilist)
        {
            foreach(Edge triedge in triangle.edges)
            {
                Debug.Log("Added edges");

                Room currentRoom = roomLocation[triedge.p1];

                float currentDistance = Vector2.Distance(triedge.p1, triedge.p2);

                Room room2 = roomLocation[triedge.p2];

                currentRoom.AddConnection(room2, currentDistance);
            }
        }
    }
}
