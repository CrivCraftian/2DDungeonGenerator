using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoomPlacer
{
    public static List<Room> GenerateRooms(int GridX, int GridY, int roomXLow, int roomYLow, int roomXHigh, int roomYHigh, int roomAmountLow, int roomAmountHigh)
    {
        int roomCount = Random.Range(roomAmountLow, roomAmountHigh);

        List<Room> rooms = new List<Room>();

        Vector2 dungeonScale = new Vector2(GridX, GridY);

        for(int i = 0; i < roomCount; i++)
        {
            bool isPlaced = false;
            int checkCount = 0;
            while(isPlaced == false)
            {
                int roomSizeX = Random.Range(roomXLow, roomXHigh);
                int roomSizeY = Random.Range(roomYLow, roomYHigh);

                Vector2 roomCheckPosition = new Vector2((int)Random.Range(0, dungeonScale.x - roomSizeX), (int)Random.Range(0, dungeonScale.y - roomSizeY));
                bool isValidPosition = true;

                foreach(Room room in rooms)
                {
                    float distanceBetweenRooms = Vector2.Distance(roomCheckPosition, room.roomPosition);
                    distanceBetweenRooms = Mathf.Abs(distanceBetweenRooms);
                    if(distanceBetweenRooms > (room.roomX * 1.5f) && distanceBetweenRooms > (room.roomY * 1.5f))
                    {
                        continue;
                    }
                    else
                    {
                        isValidPosition = false;
                    }
                }

                if(checkCount > 50)
                {
                    break;
                }

                switch(isValidPosition)
                {
                    case true:
                        rooms.Add(new Room(roomSizeX, roomSizeY, roomCheckPosition, i));
                        isPlaced = true;
                        break;
                    case false:
                        isPlaced = false;
                        checkCount++;
                        break;
                }
                
            }
        }

        return rooms;
    }

    public static void RoomsIntoGrid(Grid grid, Room[] rooms)
    {
        foreach(Room room in rooms)
        {
            for (int i = 0; i < room.roomX; i++)
            {
                for (int j = 0; j < room.roomY; j++)
                {
                    Vector2 cellPosition = new Vector2(room.roomPosition.x + i, room.roomPosition.y + j);
                    RoomCell newRoomCell = new RoomCell(cellPosition);
                    
                    if(i  == 0 && j == 0 || j == 0 && i == room.roomX-1 || i == 0 && j == room.roomY-1 || j == room.roomY-1 && i == room.roomX-1)
                    {
                        room.cornerCells.Add(newRoomCell);
                    }

                    room.roomGrid.Add(newRoomCell);

                    grid.ReplaceCell(cellPosition, newRoomCell);
                }
            }
        }
    }
}
