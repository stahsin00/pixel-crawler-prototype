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
        MakeConnections();
        InitializeRegions();

    }

    public Region GetRegion(int i) {
        // TODO: input validation
        return regions[i-1];
    }

    private void SetRegion(int x, int y, Region region) {
        RegionMap[x, y] = region.type;
        regions[region.type-1] = region;
    }

    private bool isValidMove(int x, int y, bool inclusive = false)
    {
        return (x >= 0) && (x < Size) && (y >= 0) && (y < Size) && (inclusive || RegionMap[x, y] == 0);
    }

    private void PlaceRegions() {
        int x = 0;
        int y = Random.Range(0, 4);

        SetRegion(x,y,new Region(1,x,y,true));

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
            Debug.Log($"region: {regionNum}, offset: {offset}");
            Region choice = regions[offset];
            (int, int)[] moves = { (-1, 0), (1, 0), (0, -1), (0, 1) };
            Utility.ShuffleArray(moves);

            foreach ((int, int)move in moves) {
                int x = choice.worldX + move.Item1;
                int y = choice.worldY + move.Item2;

                if (isValidMove(x, y)) {
                    SetRegion(x,y,new Region(regionNum,x,y));
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

    private void MakeConnections() {
        for (int i = 0; i < regions.Length; i++) {
            for (int j = i + 1; j < regions.Length; j++) {
                Region region1 = regions[i];
                Region region2 = regions[j];
                if (region1 == null || region2 == null) {
                    break;
                }
                if (region1.worldX == region2.worldX) {
                    int row = Random.Range(0, RegionSize);
                    int col = 0;

                    if (region1.worldY < region2.worldY) {
                        col = RegionSize-1;
                    }

                    regions[i].AddExit(new Vector2Int(row, col));
                    regions[j].AddEntrance(new Vector2Int(row, RegionSize-1 - col));
                } else if (region1.worldY == region2.worldY) {
                    int col = Random.Range(0, RegionSize);
                    int row = 0;

                    if (region1.worldX < region2.worldX) {
                        row = RegionSize-1;
                    }

                    regions[i].AddExit(new Vector2Int(row, col));
                    regions[j].AddEntrance(new Vector2Int(RegionSize-1 - row, col));
                }
            }
        }
    }

    private void InitializeRegions() {
        for (int i = 0; i < 5; i++) {
            if (regions[i] != null) {
                regions[i].Initialize();
            }
        }
    }
}
