using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    ICell[,] cells;
    Dictionary<Vector2, ICell> CellVector2 = new Dictionary<Vector2, ICell>();

    public Grid(ICell[,] inputGrid)
    {
        cells = inputGrid;

        CellVector2 = GetConnecectedCellPositions();
    }

    private Dictionary<Vector2, ICell> GetConnecectedCellPositions()
    {
        Dictionary<Vector2, ICell> PointCell = new Dictionary<Vector2, ICell>();

        for (int i = 0; i < cells.GetLength(0)-1; i++)
        {
            for(int j = 0; j < cells.GetLength(1)-1; j++)
            {
                PointCell.Add(cells[i, j].position, cells[i, j]);
            }
        }

        return PointCell;
    }

    public static void PointsToGrid(Grid grid, ICell[] points)
    {
        foreach (ICell cell in points)
        {
            Vector2 cellPosition = new Vector2(cell.position.y, cell.position.x);
            PathCell newPathCell = new PathCell(cellPosition);

            grid.ReplaceCell(cellPosition, newPathCell);
        }
    }

    public void SetCell(Vector2 position, ICell cell)
    {
        cells[(int)position.x, (int)position.y] = cell;
    }

    public ICell PointToCell(Vector2 cellPosition)
    {
        if(CellVector2.ContainsKey(cellPosition))
        {
            return CellVector2[cellPosition];
        }
        else
        {
            return null;
        }
    }

    public ICell GetCell(int x, int y)
    {
        return cells[(int)x, (int)y];
    }

    public int GetGridX()
    {
        return cells.GetLength(0);
    }

    public int GetGridY()
    {
        return cells.GetLength(1);
    }

    public void ReplaceCell(Vector2 position, ICell cellToReplace)
    {
        if(CellVector2.ContainsKey(position))
        {
            cellToReplace.SetCells(CellVector2[position].GetCells());
            CellVector2[position] = cellToReplace;
        }
    }
}
