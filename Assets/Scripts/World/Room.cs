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

    public Room() {
        chunk = new int[size,size];

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
        chunk[entrance.x, entrance.y] = 1;

        foreach (Vector2Int e in entrances) {
            FindPath(e);
        }

        foreach (Vector2Int e in exits) {
            FindPath(e);
        }

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                if (chunk[i,j] == 1) {
                    if (j != 0 && chunk[i,j - 1] > 0 && j != 3 && chunk[i,j + 1] > 0) {
                        chunk[i,j] = 4;
                    } else if (j != 0 && chunk[i,j - 1] > 0) {
                        chunk[i,j] = 2;
                    } else if (j != 3 && chunk[i,j + 1] > 0) {
                        chunk[i,j] = 3;
                    }
                }
            }
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
            chunk[position.x,position.y] = 1;
        }

        chunk[source.x,source.y] = 1;
    }
}
