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
            for(int j = 0; j < gridY; j++)
            {
                Debug.Log("Running Loop");
                GridCell currentCell = grid.GetCell(i,j);

                if(currentCell == null) continue;

                if(currentCell.CellType == CellType.RoomCell)
                {
                    foreach(GridCell cell in currentCell.GetCells())
                    {
                        if(cell.CellType == CellType.EmptyCell)
                        {
                            grid.ChangeCellType(cell, CellType.WallCell);
                            Debug.Log("Replaced Room wall");
                        }
                    }
                }

                if(currentCell.CellType == CellType.PathCell)
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
}
