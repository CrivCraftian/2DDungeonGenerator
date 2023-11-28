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

                Vector2 roomCheckPosition = new Vector2((int)Random.Range(1, dungeonScale.x - roomSizeX-1), (int)Random.Range(1, dungeonScale.y - roomSizeY-1));
                bool isValidPosition = true;

                foreach(Room room in rooms)
                {
                    float distanceBetweenRooms = Vector2.Distance(roomCheckPosition, room.roomPosition);
                    distanceBetweenRooms = Mathf.Abs(distanceBetweenRooms);
                    if(distanceBetweenRooms > (room.roomX * 1f) && distanceBetweenRooms > (room.roomY * 1f))
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

    public static void FillInEmptyRoomCells(Grid grid)
    {
        int gridX = grid.GetGridX();
        int gridY = grid.GetGridY();

        Debug.Log($"Grid X: {gridX}, Grid Y: {gridY}");

        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                Debug.Log("Running Loop");
                GridCell currentCell = grid.GetCell(i, j);

                int roomCellCount = 0;

                if(currentCell.CellType != CellType.EmptyCell)
                {
                    continue;
                }

                foreach(GridCell cell in currentCell.GetCells())
                {
                    if (cell.CellType == CellType.RoomCell)
                    {
                        roomCellCount++;
                    }
                }

                if(roomCellCount >= 3)
                {
                    currentCell.CellType = CellType.RoomCell;
                }
            }
        }
    }

    public static void RefloorRoom(Grid grid, Room room)
    {
        for (int i = 0; i < room.roomGrid.Count; i++)
        {
            grid.GetCell((int)room.roomGrid[i].x, (int)room.roomGrid[i].y).CellType = CellType.RoomCell;
        }
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
                    GridCell newRoomCell = new GridCell(cellPosition, CellType.RoomCell);
                    
                    if(i  == 0 && j == 0 || j == 0 && i == room.roomX-1 || i == 0 && j == room.roomY-1 || j == room.roomY-1 && i == room.roomX-1)
                    {
                        room.cornerCells.Add(newRoomCell.position);
                    }

                    room.roomGrid.Add(newRoomCell.position);

                    grid.ChangeCellType(grid.GetCell((int)newRoomCell.position.x, (int)newRoomCell.position.y), CellType.RoomCell);
                }
            }
        }
    }
}
