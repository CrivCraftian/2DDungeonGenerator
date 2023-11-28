using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CellType
{
    EmptyCell,
    RoomCell,
    PathCell,
    WallCell,
    StartCell
}

public interface ICell
{
    Vector2 position { get; set; }
    List<ICell> connectedCells { get; set; }

    int CellCost { get; set; }

    float gCost { get; set; }
    float hCost { get; set; }

    void AddCell(ICell cell);
    void RemoveCell(ICell cell);
    void SetCells(List<ICell> cells);
    void ClearCells();
    List<ICell> GetCells();
}

public class GridCell
{
    public CellType CellType { get; set; }

    public Vector2 position { get; set; }
    public List<GridCell> connectedCells { get; set; }

    public int CellCost { get; set; }

    public float gCost { get; set; }
    public float hCost { get; set; }



    public GridCell(Vector2 cellPosition, CellType cellType)
    {
        CellType = cellType;

        position = cellPosition;
        connectedCells = new List<GridCell>();

        CellCost = 10;
    }

    public void AddCell(GridCell cellPosition)
    {
        connectedCells.Add(cellPosition);
    }

    public void RemoveCell(GridCell cellPosition)
    {
        if (connectedCells.Contains(cellPosition))
        {
            connectedCells.Remove(cellPosition);
        }
    }

    public void SetCells(List<GridCell> cellPositions)
    {
         connectedCells = cellPositions;
    }

    public List<GridCell> GetCells()
    {
        return connectedCells;
    }

    public void ClearCells()
    {
        connectedCells.Clear();
    }

    public GridCell GetEmptyCells(Grid grid, GridCell cell)
    {
        foreach(GridCell cellPosition in cell.GetCells())
        {
            GridCell currentCell = grid.GetCell((int)cellPosition.position.x, (int)cellPosition.position.y);

            if (currentCell.CellType == CellType.EmptyCell)
            {
                return currentCell;
            }
        }

        return null;
    }
}

/*
public class RoomCell : ICell
{
    public Vector2 position { get; set; }
    public List<ICell> connectedCells { get; set; }

    public int CellCost { get; set; }

    public float gCost { get; set; }
    public float hCost { get; set; }



    public RoomCell(Vector2 cellPosition)
    {
        position = cellPosition;
        connectedCells = new List<ICell>();

        CellCost = 10;
    }

    public void AddCell(ICell cell)
    {
        connectedCells.Add(cell);
    }

    public void RemoveCell(ICell cell)
    {
        if (connectedCells.Contains(cell))
        {
            connectedCells.Remove(cell);
        }
    }

    public void SetCells(List<ICell> cells)
    {
        connectedCells = cells;
    }

    public List<ICell> GetCells()
    {
        return connectedCells;
    }

    public void ClearCells()
    {
        connectedCells.Clear();
    }
}

public class PathCell : ICell
{
    public Vector2 position { get; set; }
    public List<ICell> connectedCells { get; set; }

    public int CellCost { get; set; }

    public float gCost { get; set; }
    public float hCost { get; set; }



    public PathCell(Vector2 cellPosition)
    {
        position = cellPosition;
        connectedCells = new List<ICell>();

        CellCost = 5;
    }

    public void AddCell(ICell cell)
    {
        connectedCells.Add(cell);
    }

    public void RemoveCell(ICell cell)
    {
        if (connectedCells.Contains(cell))
        {
            connectedCells.Remove(cell);
        }
    }

    public void SetCells(List<ICell> cells)
    {
        connectedCells = cells;
    }

    public List<ICell> GetCells()
    {
        return connectedCells;
    }

    public void ClearCells()
    {
        connectedCells.Clear();
    }
}

public class WallCell : ICell
{
    public Vector2 position { get; set; }
    public List<ICell> connectedCells { get; set; }

    public int CellCost { get; set; }

    public float gCost { get; set; }
    public float hCost { get; set; }



    public WallCell(Vector2 cellPosition)
    {
        position = cellPosition;
        connectedCells = new List<ICell>();

        CellCost = 10;
    }

    public void AddCell(ICell cell)
    {
        connectedCells.Add(cell);
    }

    public void RemoveCell(ICell cell)
    {
        if (connectedCells.Contains(cell))
        {
            connectedCells.Remove(cell);
        }
    }

    public void SetCells(List<ICell> cells)
    {
        connectedCells = cells;
    }

    public List<ICell> GetCells()
    {
        return connectedCells;
    }

    public void ClearCells()
    {
        connectedCells.Clear();
    }
}
*/