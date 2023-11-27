using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{

    public static void GenerateWalls(Grid grid)
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

                if (currentCell == null) continue;

                if (currentCell.CellType == CellType.RoomCell)
                {
                    foreach (GridCell cell in currentCell.GetCells())
                    {
                        if (cell.CellType == CellType.EmptyCell)
                        {
                            grid.ChangeCellType(cell, CellType.WallCell);
                            Debug.Log("Replaced Room wall");
                        }
                    }
                }

                if (currentCell.CellType == CellType.PathCell)
                {
                    foreach (GridCell cell in currentCell.GetCells())
                    {
                        if (cell.CellType == CellType.EmptyCell)
                        {
                            grid.ChangeCellType(cell, CellType.WallCell);
                        }
                    }
                }
            }
        }
    }

    public static void GenerateWallCorners(Grid grid)
    {
        int gridX = grid.GetGridX();
        int gridY = grid.GetGridY();

        List<GridCell> cellList = new List<GridCell>();

        Debug.Log($"Grid X: {gridX}, Grid Y: {gridY}");

        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                Debug.Log("Running Loop");
                GridCell currentCell = grid.GetCell(i, j);

                if(IsValidWall(grid, currentCell) == true)
                {
                    cellList.Add(currentCell);
                }
                /*
                int surroundingWallAmount = 0;
                int surroundingEmptyAmount = 0;

                if (currentCell.CellType != CellType.WallCell)
                {
                    continue;
                }

                foreach (GridCell cell in currentCell.GetCells())
                {
                    if (cell.CellType == CellType.WallCell)
                    {
                        surroundingWallAmount++;
                    }
                }

                if (surroundingWallAmount > 1)
                {
                    continue;
                }

                foreach (GridCell cell in currentCell.GetCells())
                {
                    cellList.Add(cell);
                }

                /*
                if (surroundingWallAmount == 2 && surroundingEmptyAmount == 2)
                {
                    cellList.Add(currentCell);
                }
                */
            }
        }
        
        foreach (GridCell cell in cellList)
        {
            if (cell.CellType == CellType.EmptyCell)
            {
                cell.CellType = CellType.WallCell;
            }
        }

        /*
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                Debug.Log("Running Loop");
                GridCell currentCell = grid.GetCell(i, j);
                int surroundingWallAmount = 0;
                int surroundingPathAmount = 0;

                if (currentCell.CellType != CellType.WallCell)
                {
                    continue;
                }

                foreach (GridCell cell in currentCell.GetCells())
                {
                    if (cell.CellType == CellType.WallCell)
                    {
                        surroundingWallAmount++;
                    }
                    if (cell.CellType == CellType.PathCell)
                    {
                        surroundingPathAmount++;
                    }
                }

                if (surroundingWallAmount == 1 && surroundingPathAmount < 2)
                {
                    currentCell.CellType = CellType.EmptyCell;
                }
            }
        */
        }
    

    public static bool IsValidWall(Grid grid, GridCell cell)
    {
        Vector2Int cellPos = new Vector2Int((int)cell.position.x, (int)cell.position.y);

        if(grid.GetCell(cellPos.x + 1, cellPos.y + 1).CellType == CellType.RoomCell || grid.GetCell(cellPos.x + 1, cellPos.y + 1).CellType == CellType.PathCell)
        {
            return true;
        }
        if (grid.GetCell(cellPos.x - 1, cellPos.y + 1).CellType == CellType.RoomCell || grid.GetCell(cellPos.x - 1, cellPos.y + 1).CellType == CellType.PathCell)
        {
            return true;
        }
        if (grid.GetCell(cellPos.x + 1, cellPos.y - 1).CellType == CellType.RoomCell || grid.GetCell(cellPos.x + 1, cellPos.y - 1).CellType == CellType.PathCell)
        {
            return true;
        }
        if (grid.GetCell(cellPos.x - 1, cellPos.y - 1).CellType == CellType.RoomCell || grid.GetCell(cellPos.x - 1, cellPos.y - 1).CellType == CellType.PathCell)
        {
            return true;
        }
        return false;
    }
}


    /*
    public static void GenerateWallCorners(Grid grid)
    {
        int gridX = grid.GetGridX();
        int gridY = grid.GetGridY();

        List<GridCell> cellList = new List<GridCell>();

        Debug.Log($"Grid X: {gridX}, Grid Y: {gridY}");

        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                Debug.Log("Running Loop");
                GridCell currentCell = grid.GetCell(i, j);
                int surroundingWallAmount = 0;
                int surroundingEmptyAmount = 0;

                if(currentCell.CellType != CellType.EmptyCell)
                {
                    continue;
                }

                foreach(GridCell cell in currentCell.GetCells())
                {
                    if(cell.CellType == CellType.WallCell)
                    {
                        surroundingWallAmount++;
                    }
                    if(cell.CellType == CellType.EmptyCell)
                    {
                        surroundingEmptyAmount++;
                    }
                }

                if(surroundingWallAmount == 2 && surroundingEmptyAmount == 2)
                {
                    cellList.Add(currentCell);
                }
            }
        }

        foreach (GridCell cell in cellList)
        {
            cell.CellType = CellType.WallCell;
        }
    }
    */