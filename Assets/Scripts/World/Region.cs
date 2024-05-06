using System.Collections.Generic;
using UnityEngine;

// TODO: duplicate code
public class Region
{
    public int Type { get; private set; }
    public int WorldX { get; private set; }
    public int WorldY { get; private set; }

    public int[,] RoomMap { get; private set; }
    private Dictionary<Vector2Int, Room> rooms;

    private int size;
    private int roomSize;

    private Vector2Int entrance;
    private List<Vector2Int> entrances;
    private List<Vector2Int> exits;

    // TODO
    public int collectibles;
    public int healthBoosts;

    private int[] mainPath = { 1, 2, 2, 3, 2, 4, 5, 6, 7 };

    public Region(int type, int worldX, int worldY, int size, bool main = false, int collectibles = 0, int healthBoosts = 0)
    {
        Type = type;
        
        WorldX = worldX;
        WorldY = worldY;

        this.size = size;

        RoomMap = new int[size, size];
        rooms = new Dictionary<Vector2Int, Room>();

        this.collectibles = collectibles;
        this.healthBoosts = healthBoosts;

        if (main) {
            entrance = new Vector2Int(0,0);
        } else {
            entrance = new Vector2Int(size,size);
        }
        
        exits = new List<Vector2Int>();
        entrances = new List<Vector2Int>();
    }

    public void AddEntrance(Vector2Int entrance)
    {
        if (this.entrance.x < size && this.entrance.y < size) {
            entrances.Add(entrance);
        } else {
            this.entrance = entrance;
            AddRoom(entrance.x, entrance.y, 1);
        }
    }

    public void AddExit(Vector2Int exit)
    {
        exits.Add(exit);
    }

    private void AddRoom(int x, int y, int type = 2) {
        RoomMap[x, y] = type;

        Room room = new Room(type);
        rooms[new Vector2Int(x, y)] = room;
        MakeConnections(room);
    }

    public void Initialize()
    {
        MakePath();

        foreach (Vector2Int entrance in entrances)
        {
            FindPath(entrance, true);
        }

        foreach (Vector2Int exit in exits)
        {
            FindPath(exit);
        }

        for (int i = 0; i < collectibles; i++)
        {
            // TODO
        }

        for (int i = 0; i < healthBoosts; i++)
        {
            // TODO
        }
    }

    private void MakePath()
    {
        bool[,] visited = new bool[size, size];
        MakePathDFS(entrance.x,entrance.y,visited,mainPath.Length);

    }

    // TODO: change to stack implementation
    private bool MakePathDFS(int x, int y, bool[,] visited, int dist) {
        if (dist == 0) {
            return true;
        }

        if (visited[x,y]) {
            return false;
        }

        visited[x,y] = true;

        (int, int)[] moves = { (1, 0), (-1, 0), (0, 1), (0, -1) };
        Utility.ShuffleArray(moves);

        foreach ((int, int)move in moves) {
            int nextX = x + move.Item1;
            int nextY = y + move.Item2;

            if (!isValidMove(nextX,nextY)) {
                continue;
            }

            bool found = MakePathDFS(nextX, nextY, visited, dist-1);
            if (found) {
                AddRoom(x,y,mainPath[mainPath.Length-dist]);
                return true;
            }
        }

        visited[x,y] = false;
        return false;
    }

    private bool isValidMove(int x, int y, bool inclusive = false)
    {
        return (x >= 0) && (x < size) && (y >= 0) && (y < size) && (inclusive || RoomMap[x, y] == 0);
    }

    private void FindPath(Vector2Int source, bool isEntrance = false) 
    {
        List<Vector2Int> path = Utility.FindPath(size, source, isValidMove, (int x, int y) => { return RoomMap[x, y] > 0; });

        foreach (Vector2Int position in path)
        {
            AddRoom(position.x,position.y);
        }

        AddRoom(source.x,source.y,isEntrance ? 1 : 8);
    }

    private void MakeConnections(Room room) {
        (int, int)[] moves = { (-1, 0), (1, 0), (0, -1), (0, 1) };

        foreach ((int, int)move in moves) {
            int nextX = room.RegionX + move.Item1;
            int nextY = room.RegionY + move.Item2;

            if (isValidMove(nextX, nextY, true) && RoomMap[nextX,nextY] > 0) {
                Room neighbor = rooms[new Vector2Int(nextX,nextY)];

                int offset = Random.Range(0, roomSize);

                (int row, int col, int rowNeighbor, int colNeighbor) = CalculateCoordinates(room, neighbor, offset);

                if (room.Type < neighbor.Type) {

                    room.AddExit(new Vector2Int(row, col));
                    neighbor.AddEntrance(new Vector2Int(rowNeighbor, colNeighbor));

                } else {

                    room.AddEntrance(new Vector2Int(row, col));
                    neighbor.AddExit(new Vector2Int(rowNeighbor, colNeighbor));

                }
            }
        }
    }

    private (int row, int col, int rowNeighbor, int colNeighbor) CalculateCoordinates(Room room, Room neighbor, int offset) {
        if (room.RegionX == neighbor.RegionX) {

            int col = (room.RegionY < neighbor.RegionY) ? roomSize - 1 : 0;
            return (offset, col, offset, roomSize - 1 - col);

        } else {

            int row = (room.RegionX < neighbor.RegionX) ? roomSize - 1 : 0;
            return (row, offset, roomSize - 1 - row, offset);

        }
    }

    private void InitializeRooms() {
        foreach (Room room in rooms.Values) {

            room.Initialize();

        }
    }
}
