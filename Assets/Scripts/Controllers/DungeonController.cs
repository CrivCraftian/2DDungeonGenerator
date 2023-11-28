using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    Grid DungeonGrid;

    GridGenerator GridGenerator;

    public GameObject RoomObject;
    public GameObject TriangleObject;
    public GameObject edgeObject;
    public GameObject roomCell;
    public GameObject pathCell;
    public GameObject wallCell;
    public GameObject playerObject;

    private List<Room> rooms = new List<Room>();
    public Dictionary<Room, List<Room>> MST;

    private Vector2[] Points;

    [Header("Grid Specifications")]
    public int GridX = 50;
    public int GridY = 50;

    [Space]
    [Header("Room Specifications")]
    public int RoomX = 3;
    public int RoomY = 3;

    [Space]
    [Header("Room Amount")]
    public int roomCountLow = 1;
    public int roomCountHigh = 10;

    [Space]
    [Header("Room Size")]
    public int roomXLow = 3;
    public int roomYLow = 3;
    [Space]
    public int roomXHigh = 5;
    public int roomYHigh = 5;

    private void Awake()
    {
        GridGenerator = new GridGenerator();
    }

    // Start is called before the first frame update
    void Start()
    {
        DungeonGrid = GridGenerator.GenerateGrid(GridX, GridY);
        rooms = RoomPlacer.GenerateRooms(GridX, GridY, roomXLow, roomYLow, roomXHigh, roomYHigh, roomCountLow, roomCountHigh);
        RoomPlacer.RoomsIntoGrid(DungeonGrid, rooms.ToArray());

        /*
        foreach (Room room in rooms)
        {
            GameObject go = RoomObject;

            go.transform.localScale = new Vector3(room.roomX, room.roomY, 1);

            Instantiate(go, room.roomPosition, Quaternion.identity, this.transform);
        }
        */

        /*
        for (int i = 0; i < DungeonGrid.GetGridX(); i++)
        {
            for (int j = 0; j < DungeonGrid.GetGridY(); j++)
            {
                if(DungeonGrid.PointToCell(new Vector2(i, j)).CellType == CellType.RoomCell)
                {
                    GameObject go = Instantiate(roomCell, new Vector3(i, j, 1), Quaternion.identity, this.transform);
                }
            }
        }
        */

        Points = DelaunayTriangulation.RoomsToPoints(rooms);
        List<Triangle> triangles = DelaunayTriangulation.Triangulate(Points);

        TriangleConverter.RoomConnecterUsingTriangles(triangles.ToArray(), rooms.ToArray());

        MST = PrimsAlgorithm.RunPrim(rooms.ToArray());

        // List<GridCell> path = AStarPathfinding.RunAStarWithRooms(DungeonGrid, rooms[0], rooms[1]);
        
        foreach(KeyValuePair<Room, List<Room>> frustrating in MST)
        {
            foreach(Room rm in frustrating.Value)
            {
                List<GridCell> path = AStarPathfinding.RunAStarWithRooms(DungeonGrid, frustrating.Key, rm);

                if (path != null)
                {
                    Grid.PathToGrid(DungeonGrid, path.ToArray());
                }
                
                /*
                GameObject newConnection = Instantiate(edgeObject, Vector3.zero, Quaternion.identity, this.transform);

                EdgeScript edgeScript = newConnection.GetComponent<EdgeScript>();

                edgeScript.currentEdge = new Edge(frustrating.Key.RoomCentre, rm.RoomCentre);
                */
            }
        }
        

        WallGenerator.GenerateWalls(DungeonGrid);
        // WallGenerator.GenerateWallCorners(DungeonGrid);

        RoomPlacer.FillInEmptyRoomCells(DungeonGrid);

        // List<GridCell> path = AStarPathfinding.RunAStarWithRooms(DungeonGrid, MST.ElementAt(0).Key, MST.ElementAt(0).Value[0]);

        // Grid.PointsToGrid(DungeonGrid, path.ToArray());

        foreach (Room room in rooms)
        {
            RoomPlacer.RefloorRoom(DungeonGrid, room);
        }

        for (int i = 0; i < DungeonGrid.GetGridX()-1; i++)
        {
            for (int j = 0; j < DungeonGrid.GetGridY()-1; j++)
            {
                if (DungeonGrid.PointToCell(new Vector2(i, j)).CellType == CellType.RoomCell)
                {
                    GameObject go = Instantiate(roomCell, new Vector3(i, j, 1), Quaternion.identity, this.transform);
                }
                if(DungeonGrid.PointToCell(new Vector2(i, j)).CellType == CellType.PathCell)
                {
                    GameObject go = Instantiate(pathCell, new Vector3(i, j, 1), Quaternion.identity, this.transform);
                }
                if(DungeonGrid.PointToCell(new Vector2(i, j)).CellType == CellType.WallCell)
                {
                    GameObject go = Instantiate(wallCell, new Vector3(i, j, 1), Quaternion.identity, this.transform);
                }
            }
        }

        /*
        foreach(Triangle triangle in triangles)
        {
            GameObject currentTri = Instantiate(this.TriangleObject, new Vector3(0, 0, 0), Quaternion.identity, this.transform);

            TriangleObject triScript = currentTri.GetComponent<TriangleObject>();

            triScript.connectedTri = triangle;
        }
        */

        int randomRoom = UnityEngine.Random.Range(0, rooms.Count - 1);
        Instantiate(playerObject, rooms[randomRoom].roomPosition, Quaternion.identity);

        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
