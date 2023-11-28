using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    public Tilemap dungeonTilemap;
    public Tile FloorTile;
    public Tile WallTile;

    public Grid grid;

    /*
    public TilemapGenerator(Grid grid)
    {
        this.grid = grid;
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateDungeonTilemap()
    {
        int dungeonX = grid.GetGridX();
        int dungeonY = grid.GetGridY();

        dungeonTilemap.size = new Vector3Int(dungeonX-1, dungeonY-1);

        for (int i = 0; i < dungeonX-1; i++)
        {
            for (int j = 0; j < dungeonY-1; j++)
            {
                Vector3Int tilePosition = new Vector3Int(i, j, 0);

                if(grid.GetCell(i, j).CellType == CellType.WallCell)
                {
                    dungeonTilemap.SetTile(tilePosition, WallTile);
                }
                if (grid.GetCell(i, j).CellType == CellType.RoomCell || grid.GetCell(i, j).CellType == CellType.PathCell)
                {
                    dungeonTilemap.SetTile(tilePosition, FloorTile);
                }
            }
        }
    }
}
