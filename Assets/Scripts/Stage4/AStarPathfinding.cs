using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public static class AStarPathfinding
{
    public static List<ICell> RunAStarWithRooms(Grid grid, Room StartRoom, Room EndRoom)
    {
        RoomCell startCell = (RoomCell)closestCell(StartRoom, EndRoom.RoomCentre);
        RoomCell goalCell = (RoomCell)closestCell(EndRoom, StartRoom.RoomCentre);

        Debug.Log($"Start Cell Position: {startCell.position}");
        Debug.Log($"End Cell Position: {goalCell.position}");

        PriorityQueue<ICell> openset = new PriorityQueue<ICell>();
        List<ICell> closedset = new List<ICell>();

        Dictionary<ICell, ICell> parentList = new Dictionary<ICell, ICell>();

        startCell.gCost = 0;
        startCell.hCost = (int)CalculateManhattanDistance(startCell.position, goalCell.position) * 10;

        openset.Enqueue(startCell, 0);

        while(openset.Count()>0)
        {
            ICell currentCell = openset.Dequeue();

            closedset.Add(currentCell);

            if(Vector2.Distance(currentCell.position, goalCell.position) < 0.1f)
            {
                parentList[goalCell] = currentCell;
                Debug.Log("FoundPath");
                return ReconstructPath(startCell, goalCell, parentList);
            }

            foreach(ICell neibhorCell in currentCell.GetCells())
            {
                if(closedset.Contains(neibhorCell) || neibhorCell is RoomCell)
                {
                    Debug.Log("Passed");
                    continue; 
                }

                float newGCost = currentCell.gCost + neibhorCell.CellCost;

                Debug.Log(newGCost);

                if(openset.Contains(neibhorCell))
                {
                    neibhorCell.gCost = newGCost;
                    neibhorCell.hCost = CalculateEuclideanDistance(neibhorCell.position, goalCell.position) * 10;
                    parentList[neibhorCell] = currentCell;

                    float newPriority = (neibhorCell.gCost + neibhorCell.hCost);

                    if (openset.GetPriority(neibhorCell) > newPriority)
                    {
                        openset.SetPriority(neibhorCell, newPriority);
                    }
                }
                else
                {
                    neibhorCell.gCost = newGCost;
                    neibhorCell.hCost = CalculateEuclideanDistance(neibhorCell.position, goalCell.position) * 10;

                    parentList[neibhorCell] = currentCell;

                    float fCost = neibhorCell.gCost + neibhorCell.hCost;

                    Debug.Log($"F cost {fCost}");

                    openset.Enqueue(neibhorCell, fCost);
                }
            }
        }

        return null;
    }

    private static List<ICell> ReconstructPath(ICell startCell, ICell goalCell, Dictionary<ICell, ICell> parents)
    {
        List<ICell> path = new List<ICell>();
        ICell current = goalCell;
        
        while (current != startCell)
        {
            path.Add(current);
            current = parents[current];
        }

        path.Remove(goalCell);
        path.Reverse();

        return path;
    }

    private static float CalculateManhattanDistance(Vector2 point1, Vector2 point2)
    {
        return Mathf.Abs(point1.x - point2.x) + Mathf.Abs(point1.y - point2.y);
    }

    private static int CalculateEuclideanDistance(Vector2 point1, Vector2 point2)
    {
        return (int)Vector2.Distance(point1, point2);
    }

    private static ICell closestCell(Room room, Vector2 pos)
    {
        float Distance = Mathf.Infinity;
        RoomCell bestCell = null;
        foreach(RoomCell cell in room.roomGrid)
        {
            float currentDistance = Vector2.Distance(cell.position, pos);

            if(room.cornerCells.Contains(cell))
            {
                continue;
            }
            else if(currentDistance<Distance)
            {
                Distance = currentDistance;
                bestCell = cell;
            }
        }

        return bestCell;
    }

    /*
public static List<GridCell> Funky(Grid grid, Room StartRoom, Room EndRoom)
{
    RoomCell startCell = (RoomCell)closestCell(StartRoom, EndRoom);
    RoomCell goalCell = (RoomCell)closestCell(EndRoom, StartRoom);



    PriorityQueue<GridCell> openset = new PriorityQueue<GridCell>();
    List<GridCell> closeset = new List<GridCell>();

    Dictionary<GridCell, GridCell> parentDic = new Dictionary<GridCell, GridCell>();

    openset.Enqueue(startCell, 0);

    float costSinceStart = 0;

    while (openset.Count() > 0)
    {
        GridCell cell = openset.Dequeue();
        costSinceStart += cell.CellCost;

        closeset.Add(cell);

        List<GridCell> cells = cell.GetCells();

        foreach (GridCell nCell in cells)
        {
            if (nCell is RoomCell)
            {
                continue;
            }
            else
            {
                if (!openset.Contains(nCell))
                {
                    nCell.gCost = cell.CellCost;
                    nCell.hCost = CalculateManhattanDistance(nCell.position, goalCell.position);
                    openset.Enqueue(nCell, nCell.gCost + nCell.hCost);
                }
                else
                {
                    nCell.gCost = cell.CellCost;
                    nCell.hCost = CalculateManhattanDistance(nCell.position, goalCell.position);
                    float fCost = nCell.gCost + nCell.hCost;

                    if (fCost < openset.GetPriority(nCell))
                    {
                        openset.SetPriority(nCell, fCost);
                    }
                }
            }
        }

        if (cell == goalCell)
        {
            return closeset;
        }
    }

    return null;
}
*/
}
