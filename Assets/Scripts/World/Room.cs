using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Room
{
    public int Type { get; private set; }
    public int size = 4;
    public int chunkSize = 5;
    public int[,] chunkMap;
    public Chunk[,] chunks;

    public int RegionX { get; private set; }
    public int RegionY { get; private set; }

    public Region RoomRegion { get; private set; }

    private Vector2Int entrance;
    private List<Vector2Int> entrances;
    private List<Vector2Int> exits;

    public bool Discovered { get; private set; }

    private bool spawn;

    public Room(int x, int y, Region region, int type = 2, bool spawn = false) {
        chunkMap = new int[size,size];
        chunks = new Chunk[size,size];

        RoomRegion = region;

        Type = type;

        RegionX = x;
        RegionY = y;

        Discovered = true;

        this.spawn = spawn;

        if (spawn) {
            entrance = new Vector2Int(Random.Range(0,size),Random.Range(0,size));
            AddChunk(entrance.x,entrance.y);
        } else {
            entrance = new Vector2Int(size,size);
        }

        entrances = new List<Vector2Int>();
        exits = new List<Vector2Int>();
    }

    private void AddChunk(int x, int y) {
        if (chunkMap[x,y] > 0) return;
        chunkMap[x,y] = 1;
        chunks[x,y] = new Chunk(chunkSize);
        chunks[x,y].SetType(1);
    }

    public void AddEntrance(Vector2Int position) {
        if (entrance.x < size && entrance.y < size) {
            entrances.Add(position);
        } else {
            entrance = position;
        }
    }

    public void AddExit(Vector2Int position) {
        exits.Add(position);
    }

    public void Initialize() {

        if (entrance.x < size && entrance.y < size) {
            SetRoom(entrance.x, entrance.y);
        } else {
            SetRoom(exits[0].x, exits[0].y);
        }

        foreach (Vector2Int e in entrances) {
            FindPath(e);
        }

        foreach (Vector2Int e in exits) {
            FindPath(e);
        }

        if (spawn) {chunks[entrance.x,entrance.y].SetSpawn();}

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                if (chunks[i,j] != null) {
                    chunks[i,j].Initialize();
                }
            }
        }
    }

    private void SetRoom(int x, int y) {
        AddChunk(x,y);

        if (isValidMove(x,y-1,true) && chunkMap[x,y-1] > 0) {
            chunkMap[x,y-1] = chunkMap[x,y-1] == 3 ? 4 : 2;
            chunks[x,y-1].SetType(chunkMap[x,y-1]);

            chunkMap[x,y] = chunkMap[x,y] == 2 ? 4 : 3;
            chunks[x,y].SetType(chunkMap[x,y]);
        }

        if (isValidMove(x,y+1,true) && chunkMap[x,y+1] > 0) {
            chunkMap[x,y+1] = chunkMap[x,y+1] == 2 ? 4 : 3;
            chunks[x,y+1].SetType(chunkMap[x,y+1]);

            chunkMap[x,y] = chunkMap[x,y] == 3 ? 4 : 2;
            chunks[x,y].SetType(chunkMap[x,y]);
        }
    }

    private bool isValidMove(int x, int y, bool inclusive = false)
    {
        return (x >= 0) && (x < size) && (y >= 0) && (y < size) && (inclusive || chunkMap[x, y] == 0);
    }

    private void FindPath(Vector2Int source) 
    {
        List<Vector2Int> path = Utility.FindPath(size, source, isValidMove, (int x, int y) => { return chunkMap[x, y] > 0; });

        foreach (Vector2Int position in path)
        {
            SetRoom(position.x,position.y);
        }

        SetRoom(source.x,source.y);
    }

    public int this[int row, int col] {
        get 
        { 
            if (chunks[row / chunkSize, col / chunkSize] != null)
                return chunks[row / chunkSize, col / chunkSize][row % chunkSize, col % chunkSize]; 
            else 
                return 1;
        }
    }
}
