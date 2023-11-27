using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator
{
    public Grid GenerateGrid(int gridX, int gridY)
    {
        GridCell[,] GeneratedCells = new GridCell[gridX, gridY];

        for (int i = 0; i < gridX; i++)
        {
            for(int j = 0; j < gridY; j++)
            {
                GeneratedCells[i,j] = new GridCell(new Vector2(i, j), CellType.EmptyCell);
            }
        }

        for (int k = 0; k < gridX; k++)
        {
            for (int l = 0; l < gridY; l++)
            {
                if (k > 0)
                {
                    GeneratedCells[k, l].AddCell(GeneratedCells[k - 1, l]);
                }
                if (k < gridX - 1)
                {
                    GeneratedCells[k, l].AddCell(GeneratedCells[k + 1, l]);
                }
                if (l > 0)
                {
                    GeneratedCells[k, l].AddCell(GeneratedCells[k, l - 1]);
                }
                if (l < gridY - 1)
                {
                    GeneratedCells[k, l].AddCell(GeneratedCells[k, l + 1]);
                }
            }
        }

        return new Grid(GeneratedCells);
    }
}
