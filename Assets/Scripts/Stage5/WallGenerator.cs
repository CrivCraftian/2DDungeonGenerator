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
                ICell currentCell = grid.GetCell(i,j);

                if(currentCell == null) continue;

                if(currentCell is RoomCell)
                {
                    foreach(ICell cell in currentCell.GetCells())
                    {
                        if(!(cell is not RoomCell) || !(cell is not PathCell))
                        {
                            grid.ReplaceCell(cell.position, new WallCell(cell.position));
                            Debug.Log("Replaced Room wall");
                        }
                    }
                }

                if(currentCell is PathCell)
                {
                    foreach (ICell cell in currentCell.GetCells())
                    {
                        if (cell is GridCell)
                        {
                            grid.ReplaceCell(cell.position, new WallCell(cell.position));
                        }
                    }
                }
            }
        }
    }
}
