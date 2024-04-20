using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int size = 4;
    public int[,] chunk;

    private Vector2Int entrance;
    private List<Vector2Int> entrances;
    private List<Vector2Int> exits;

    public bool Discovered { get; private set; }

    public Room() {
        chunk = new int[size,size];
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
    }

    private void SetRoom(int x, int y) {
        chunk[x,y] = 1;

        if (isValidMove(x,y-1,true) && chunk[x,y-1] > 0) {
            chunk[x,y-1] = chunk[x,y-1] > 1 ? 4 : 2;
            chunk[x,y] = 3;
        }

        if (isValidMove(x,y+1,true) && chunk[x,y+1] > 0) {
            chunk[x,y+1] = chunk[x,y-1] > 1 ? 4 : 3;
            chunk[x,y] = chunk[x,y] > 1 ? 4 : 2;
        }
    }

    private bool isValidMove(int x, int y, bool inclusive = false)
    {
        return (x >= 0) && (x < size) && (y >= 0) && (y < size) && (inclusive || chunk[x, y] == 0);
    }

    private void FindPath(Vector2Int source) 
    {
        
        List<Vector2Int> path = Utility.FindPath(size, source, isValidMove, (int x, int y) => { return chunk[x, y] > 0; });

        foreach (Vector2Int position in path)
        {
            SetRoom(position.x,position.y);
        }

        SetRoom(source.x,source.y);
    }
}
