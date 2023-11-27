using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public static class AStarPathfinding
{
    public static List<GridCell> RunAStarWithRooms(Grid grid, Room StartRoom, Room EndRoom)
    {
        GridCell startCell = closestCell(grid, StartRoom, EndRoom.RoomCentre); //.GetEmptyCells(grid);
        GridCell goalCell = closestCell(grid, EndRoom, StartRoom.RoomCentre); //.GetEmptyCells(grid);

        // goalCell.CellType = CellType.EmptyCell;

        Debug.Log($"Start Cell Position: {startCell.position}");
        Debug.Log($"End Cell Position: {goalCell.position}");
        PriorityQueue<GridCell> openset = new PriorityQueue<GridCell>();
        HashSet<GridCell> closedset = new HashSet<GridCell>();
        Dictionary<GridCell, GridCell> parentList = new Dictionary<GridCell, GridCell>();

        startCell.gCost = 0;
        startCell.hCost = (int)CalculateManhattanDistance(startCell.position, goalCell.position) * 10;

        startCell.CellType = CellType.StartCell;

        openset.Enqueue(startCell, 0);

        while (openset.Count() > 0)
        {
            GridCell currentCell = openset.Dequeue();

            closedset.Add(currentCell);

            /*
            if (Vector2.Distance(currentCell.position, goalCell.position) < 0.1f)
            {
                parentList[goalCell] = currentCell;
                Debug.Log("FoundPath");
                foreach(KeyValuePair<GridCell, GridCell> dic  in parentList)
                {
                    Debug.Log($"{dic.Key.position} = {dic.Value.position}");
                }
                return ReconstructPath(grid, startCell, goalCell, parentList);
            }
            */

            foreach (GridCell neighborCell in currentCell.GetCells())
            {
                // Debug.Log(neighborCell.position);
                Debug.Log("Iterating Neighbors");

                if (Vector2.Distance(neighborCell.position, goalCell.position) < 0.1f)
                {
                    parentList[goalCell] = currentCell;
                    Debug.Log("FoundPath");
                    foreach (KeyValuePair<GridCell, GridCell> dic in parentList)
                    {
                        // Debug.Log($"{dic.Key.position} = {dic.Value.position}");
                    }
                    return ReconstructPath(grid, startCell, goalCell, parentList);
                }
                else if (closedset.Contains(neighborCell) || neighborCell.CellType == CellType.RoomCell)
                {
                    Debug.Log("Passed");
                    continue;
                }

                float newGCost = currentCell.gCost + neighborCell.CellCost;

                if (openset.Contains(neighborCell))
                {
                    neighborCell.gCost = newGCost;
                    neighborCell.hCost = CalculateManhattanDistance(neighborCell.position, goalCell.position) * 10;
                    parentList[neighborCell] = currentCell;

                    float newPriority = neighborCell.gCost + neighborCell.hCost;

                    if (openset.GetPriority(neighborCell) > newPriority)
                    {
                        openset.SetPriority(neighborCell, newPriority);
                    }
                }
                else
                {
                    neighborCell.gCost = newGCost;
                    neighborCell.hCost = CalculateManhattanDistance(neighborCell.position, goalCell.position) * 10;

                    parentList[neighborCell] = currentCell;

                    float fCost = neighborCell.gCost + neighborCell.hCost;

                    // Debug.Log($"F cost {fCost}");

                    openset.Enqueue(neighborCell, fCost);
                }
            }
        }

        Debug.Log("Pathfinding Failed");
        return null;
    }

    private static List<GridCell> ReconstructPath(Grid grid, GridCell startCell, GridCell goalCell, Dictionary<GridCell, GridCell> parents)
    {
        int count = 0;

        List<GridCell> path = new List<GridCell>();
        GridCell current = goalCell;

        Debug.Log("BeforePathCreation");

        
        while (current.CellType != startCell.CellType /* ||  count < 100*/)
        {
            //Debug.Log("Before Add");
            path.Add(current);
            //
            //
            current = parents[current];
            Debug.Log("Current Position: " + current.position);

            count++;
        }

        path.Remove(goalCell);
        // path.Remove(startCell);

        startCell.CellType = CellType.RoomCell;

        path.Reverse();

        if(path.Count < 2)
        {
            return null;
        }

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

    private static GridCell closestCell(Grid grid, Room room, Vector2 pos)
    {
        float Distance = Mathf.Infinity;
        Vector2 bestCell = new Vector2(0, 0);
        foreach(Vector2 cellPos in room.roomGrid)
        {
            float currentDistance = Vector2.Distance(cellPos, pos);

            if(room.cornerCells.Contains(cellPos))
            {
                continue;
            }
            else if(currentDistance<Distance)
            {
                Distance = currentDistance;
                bestCell = cellPos;
            }
        }

        return grid.GetCell((int)bestCell.x, (int)bestCell.y);
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
