using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    GridCell[,] cells;
    Dictionary<Vector2, GridCell> CellVector2 = new Dictionary<Vector2, GridCell>();

    public Grid(GridCell[,] inputGrid)
    {
        cells = inputGrid;

        CellVector2 = GetConnectedCellPositions();
    }

    private Dictionary<Vector2, GridCell> GetConnectedCellPositions()
    {
        Dictionary<Vector2, GridCell> PointCell = new Dictionary<Vector2, GridCell>();

        for (int i = 0; i < cells.GetLength(0)-1; i++)
        {
            for(int j = 0; j < cells.GetLength(1)-1; j++)
            {
                PointCell.Add(cells[i, j].position, cells[i, j]);
            }
        }

        return PointCell;
    }

    public static void PathToGrid(Grid grid, GridCell[] points)
    {
        foreach (GridCell cell in points)
        {
            // Vector2 cellPosition = new Vector2(cell.position.y, cell.position.x);

            grid.ChangeCellType(grid.GetCell((int)cell.position.x, (int)cell.position.y), CellType.PathCell);
        }
    }

    public void SetCell(Vector2 position, GridCell cell)
    {
        cells[(int)position.x, (int)position.y] = cell;
    }

    public GridCell PointToCell(Vector2 cellPosition)
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

    public static GridCell GetEmptyCells(Grid grid, GridCell cell)
    {
        foreach (GridCell cellPosition in cell.GetCells())
        {
            GridCell currentCell = grid.GetCell((int)cellPosition.position.x, (int)cellPosition.position.y);

            if (currentCell.CellType == CellType.EmptyCell)
            {
                return currentCell;
            }
        }

        return null;
    }

    public GridCell GetCell(int x, int y)
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

    public void ChangeCellType(GridCell cell, CellType type)
    {
        if(CellVector2.ContainsKey(cell.position))
        {
            cells[(int)cell.position.x, (int)cell.position.y].CellType = type;
        }
    }
}
