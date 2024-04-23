using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int size = 4;
    public int chunkSize = 5;
    public int[,] chunkMap;
    public Chunk[,] chunks;

    private Vector2Int entrance;
    private List<Vector2Int> entrances;
    private List<Vector2Int> exits;

    public bool Discovered { get; private set; }

    public Room() {
        chunkMap = new int[size,size];
        chunks = new Chunk[size,size];

        Discovered = true;

        // temp
        Vector2Int e1 = new Vector2Int(0,2);
        Vector2Int e2 = new Vector2Int(3,1);
        Vector2Int e3 = new Vector2Int(2,3);

        entrance = e1;

        entrances = new List<Vector2Int>();
        exits = new List<Vector2Int>();

        entrances.Add(e2);
        exits.Add(e3);

        Initialize();
    }

    public void Initialize() {
        SetRoom(entrance.x, entrance.y);

        foreach (Vector2Int e in entrances) {
            FindPath(e);
        }

        foreach (Vector2Int e in exits) {
            FindPath(e);
        }

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                if (chunks[i,j] != null) {
                    chunks[i,j].Initialize();
                }
            }
        }
    }

    private void SetRoom(int x, int y) {
        chunkMap[x,y] = 1;
        chunks[x,y] = new Chunk(chunkSize);
        chunks[x,y].SetType(1);

        if (isValidMove(x,y-1,true) && chunkMap[x,y-1] > 0) {
            chunkMap[x,y-1] = chunkMap[x,y-1] == 3 ? 4 : 2;
            chunks[x,y-1].SetType(chunkMap[x,y-1]);

            chunkMap[x,y] = chunkMap[x,y] == 2 ? 4 : 3;
            chunks[x,y].SetType(chunkMap[x,y]);
        }

        if (isValidMove(x,y+1,true) && chunkMap[x,y+1] > 0) {
            chunkMap[x,y+1] = chunkMap[x,y-1] == 2 ? 4 : 3;
            chunks[x,y+1].SetType(chunkMap[x,y-1]);

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
