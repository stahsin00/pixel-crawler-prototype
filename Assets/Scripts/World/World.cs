using UnityEngine;

public class World
{
    public int Size { get; private set; }
    public int RegionSize { get; private set; }

    public int[,] RegionMap { get; private set; }
    private Region[] regions;

    // TODO
    private float fillerChance = 0.75f;
    private int maxFillerCount = 2;

    public World()
    {
        Size = 4;
        RegionSize = 4;

        RegionMap = new int[Size, Size];
        regions = new Region[3 + maxFillerCount];

        Initialize();
    }

    private void Initialize() {
        
        PlaceRegions();
        InitializeRegions();

    }

    public Region GetRegion(int i) {
        // TODO: input validation
        return regions[i-1];
    }

    private void SetRegion(int x, int y, Region region) {
        RegionMap[x, y] = region.RegionType;
        regions[region.RegionType-1] = region;
        MakeConnections(region);
    }

    private bool isValidMove(int x, int y, bool inclusive = false)
    {
        return (x >= 0) && (x < Size) && (y >= 0) && (y < Size) && (inclusive || RegionMap[x, y] == 0);
    }

    private void PlaceRegions() {
        int x = 0;
        int y = Random.Range(0, Size);

        SetRegion(x,y,new Region(1,x,y,RegionSize,true));

        PlaceRegion(2);
        PlaceRegion(3);

        for (int i = 0; i < maxFillerCount; i++) {
            if (Random.value < fillerChance) {
                PlaceRegion(4 + i);
            } else {
                break;
            }
        }
    }

    private void PlaceRegion(int regionNum) {
        bool placed = false;

        int pos = Random.Range(0, regionNum-1);
        int offset = pos;

        do {
            Region choice = regions[offset];
            (int, int)[] moves = { (-1, 0), (1, 0), (0, -1), (0, 1) };
            Utility.ShuffleArray(moves);

            foreach ((int, int)move in moves) {
                int x = choice.WorldX + move.Item1;
                int y = choice.WorldY + move.Item2;

                if (isValidMove(x, y)) {
                    SetRegion(x,y,new Region(regionNum,x,y,RegionSize));
                    placed = true;
                    break;
                }
            }

            offset = (offset + 1) % (regionNum-1);

        } while (!placed && offset != pos);

        // TODO : technically code should never get here...
        if (!placed) {
            Debug.LogError($"Unable to place region {regionNum}.");
        }
    }

    private void MakeConnections(Region region) {
        (int, int)[] moves = { (-1, 0), (1, 0), (0, -1), (0, 1) };

        foreach ((int, int)move in moves) {
            int nextX = region.WorldX + move.Item1;
            int nextY = region.WorldY + move.Item2;

            if (isValidMove(nextX, nextY, true) && RegionMap[nextX,nextY] > 0) {
                Region neighbor = GetRegion(RegionMap[nextX,nextY]);

                int offset = Random.Range(0, RegionSize);

                (int row, int col, int rowNeighbor, int colNeighbor) = CalculateCoordinates(region, neighbor, offset);

                // TODO: temp chunk connection
                if (region.RegionType < neighbor.RegionType) {

                    region.AddExit(new Vector2Int(row, col),new Vector2Int(row, col));
                    neighbor.AddEntrance(new Vector2Int(rowNeighbor, colNeighbor),new Vector2Int(rowNeighbor, colNeighbor));

                } else {

                    region.AddEntrance(new Vector2Int(row, col),new Vector2Int(row, col));
                    neighbor.AddExit(new Vector2Int(rowNeighbor, colNeighbor),new Vector2Int(rowNeighbor, colNeighbor));

                }
            }
        }
    }

    private (int row, int col, int rowNeighbor, int colNeighbor) CalculateCoordinates(Region region, Region neighbor, int offset) {
        if (region.WorldX == neighbor.WorldX) {

            int col = (region.WorldY < neighbor.WorldY) ? RegionSize - 1 : 0;
            return (offset, col, offset, RegionSize - 1 - col);

        } else {

            int row = (region.WorldX < neighbor.WorldX) ? RegionSize - 1 : 0;
            return (row, offset, RegionSize - 1 - row, offset);

        }
    }

    public Room GetSpawn() {
        // TODO
        return regions[0].GetRoom(0,0);
    }

    public Room GetAdjacentRoom(Room room, int x, int y) {
        if (room.RegionX + x < 0 || room.RegionX + x >= room.RoomRegion.Size || room.RegionY + y < 0 || room.RegionY + y >= room.RoomRegion.Size) {
            Region adjacent;

            if (room.RegionX + x < 0) {
                adjacent = regions[RegionMap[room.RoomRegion.WorldX+x, room.RoomRegion.WorldY]];
                return adjacent.GetRoom(adjacent.Size-1,room.RegionY);
            } else if (room.RegionX + x >= room.RoomRegion.Size) {
                adjacent = regions[RegionMap[room.RoomRegion.WorldX+x, room.RoomRegion.WorldY]];
                return adjacent.GetRoom(0,room.RegionY);
            } else if (room.RegionY + y < 0) {
                adjacent = regions[RegionMap[room.RoomRegion.WorldX, room.RoomRegion.WorldY+y]];
                return adjacent.GetRoom(room.RegionX,adjacent.Size-1);
            } else {
                adjacent = regions[RegionMap[room.RoomRegion.WorldX, room.RoomRegion.WorldY+y]];
                return adjacent.GetRoom(room.RegionX,0);
            }     
        }

        return room.RoomRegion.GetRoom(room.RegionX + x, room.RegionY + y);
    }

    private void InitializeRegions() {
        for (int i = 0; i < regions.Length; i++) {

            if (regions[i] == null) {
                return;
            }

            regions[i].Initialize();

        }
    }
}
