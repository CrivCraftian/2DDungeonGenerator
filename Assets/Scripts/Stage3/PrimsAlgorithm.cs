using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrimsAlgorithm
{
    public static Dictionary<Room, List<Room>> RunPrim(Room[] rooms)
    {
        if(rooms.Length == 0)
            return null;

        Dictionary<Room, List<Room>> MST = new Dictionary<Room, List<Room>> ();

        Stack<Room> stack = new Stack<Room>();
        stack.Push(rooms[0]);

        HashSet<Room> visitedRooms = new HashSet<Room>();

        while (visitedRooms.Count < rooms.Length)
        {
            Room currentRoom = stack.Pop();

            Room bestChoice = null;
            float lowestValue = Mathf.Infinity;

            visitedRooms.Add(currentRoom);

            foreach (KeyValuePair<Room, float> connectedRoom in currentRoom.GetConnections())
            {
                if (visitedRooms.Contains(connectedRoom.Key))
                {
                    continue;
                }
                else if (connectedRoom.Value < lowestValue)
                {
                    lowestValue = connectedRoom.Value;
                    bestChoice = connectedRoom.Key;
                }
            }

            if (bestChoice != null)
            {
                if (MST.ContainsKey(currentRoom))
                {
                    MST[currentRoom].Add(bestChoice);
                }
                else {
                    MST.Add(currentRoom, new List<Room>() { bestChoice });
                }

                stack.Push(currentRoom);
                stack.Push(bestChoice);
            }
        }

        return MST;
    }
}
