using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// idr wtf is going on here but ill fix it eventually i swear
public class Region
{
    public int type;
    public int worldX;
    public int worldY;

    public int size = 4;
    public int[,] room;

    private Vector2Int entrance;
    private List<Vector2Int> entrances;
    private List<Vector2Int> exits;

    // T-T
    private int[] mainPath1 = { 1, 2, 2, 3, 2, 4, 5, 6, 7 };
    private int[] mainPath2 = { 1, 2, 2, 2, 3, 2, 4, 5, 6, 7 };
    private int[] mainPath3 = { 1, 2, 2, 3, 2, 2, 4, 5, 6, 7 };
    private int[] mainPath4 = { 1, 2, 2, 2, 3, 2, 2, 4, 5, 6, 7 };

    public int collectibles;
    public int healthBoosts;

    public Region(int type, int worldX, int worldY, bool main = false, int collectibles = 0, int healthBoosts = 0)
    {
        this.type = type;
        
        this.worldX = worldX;
        this.worldY = worldY;

        room = new int[size, size];
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
            room[entrance.x, entrance.y] = 1;
        }
    }

    public void AddExit(Vector2Int exit)
    {
        exits.Add(exit);
    }

    public void Initialize()
    {
        MakeMainPath();

        // foreach (Vector2Int exit in exits)
        // {
        //     FindPath(exit);
        // }

        // for (int i = 0; i < collectibles; i++)
        // {
        //     FindPath(0);
        // }

        // for (int i = 0; i < healthBoosts; i++)
        // {
        //     FindPath(1);
        // }
    }

    private void MakeMainPath()
    {
        //MainPathBFS(entrance);
        List<Vector2Int> path = MakeMainPathHelper(entrance, mainPath1.Length);

        foreach (Vector2Int position in path)
        {
            //Debug.Log($"Region {type} Position: ({position.x},{position.y})");
            room[position.x,position.y] = 1;
        }

        room[entrance.x,entrance.y] = 1;

        // for (int i = 0; i < size; i++){
        //     string row = "";
        //     for (int j = 0; j < size; j++) {
        //         row += $"{room[i,j]} ";
        //     }
        //     Debug.Log(row);
        // }
    }

    private List<Vector2Int> MakeMainPathHelper(Vector2Int source, int max) {
        int[,] dist = new int[size,size];
        Vector2Int[,] prev = new Vector2Int[size,size];
        PriorityQueue<Vector2Int> priority = new PriorityQueue<Vector2Int>();

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                dist[i,j] = size * size * 2;
                prev[i,j] = new Vector2Int(size, size);
                priority.Enqueue(new Vector2Int(i,j), dist[i,j]);
            }
        }

        dist[source.x,source.y] = 0;
        priority.UpdatePriority(source,0);

        Vector2Int cur = source;

        while (priority.Count > 0) {
            cur = priority.Dequeue();

            if (cur != source) {
                Vector2Int prevNode = prev[cur.x,cur.y];
                if (dist[prevNode.x,prevNode.y] == max - 1) {
                    break;
                }
            }
            
            foreach (var (dx, dy) in new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) }) {
                var newX = cur.x + dx;
                var newY = cur.y + dy;
                if (isValidMove(newX, newY, true) && (dist[newX, newY] > dist[cur.x, cur.y] + 1)) {
                    dist[newX, newY] = dist[cur.x, cur.y] + 1;
                    prev[newX, newY] = cur;
                    priority.UpdatePriority(new Vector2Int(newX, newY), dist[newX, newY]);
                }
            }
        }

        List<Vector2Int> path = new List<Vector2Int>();

        while (cur != source)
        {
            path.Add(cur);
            cur = prev[cur.x,cur.y];
        }

        path.Reverse();

        return path;
    }

    private bool isValidMove(int x, int y, bool inclusive = false)
    {
        return (x >= 0) && (x < size) && (y >= 0) && (y < size) && (inclusive || room[x, y] == 0);
    }

    // genuinely wtf
    private void MainPathBFS(Vector2Int entrance)
    {
        Queue<List<(int, int)>> solution;
        List<(int, int)> path;

        int type = Random.Range(0, 4);
        int index;

        switch (type)
        {
            case 0:
                solution = MainPathBFSHelper(entrance, mainPath1.Length);
                index = Random.Range(0, solution.Count - 1);
                path = solution.Dequeue();
                while (index > 1)
                {
                    path = solution.Dequeue();
                    index--;
                }
                for (int i = 0; i < path.Count; i++)
                {
                    room[path[i].Item1, path[i].Item2] = mainPath1[i];
                }
                break;
            case 1:
                solution = MainPathBFSHelper(entrance, mainPath2.Length);
                index = Random.Range(0, solution.Count - 1);
                path = solution.Dequeue();
                while (index > 1)
                {
                    path = solution.Dequeue();
                    index--;
                }
                for (int i = 0; i < path.Count; i++)
                {
                    room[path[i].Item1, path[i].Item2] = mainPath2[i];
                }
                break;
            case 2:
                solution = MainPathBFSHelper(entrance, mainPath3.Length);
                index = Random.Range(0, solution.Count - 1);
                path = solution.Dequeue();
                while (index > 1)
                {
                    path = solution.Dequeue();
                    index--;
                }
                for (int i = 0; i < path.Count; i++)
                {
                    room[path[i].Item1, path[i].Item2] = mainPath3[i];
                }
                break;
            default:
                solution = MainPathBFSHelper(entrance, mainPath3.Length);
                index = Random.Range(0, solution.Count - 1);
                path = solution.Dequeue();
                while (index > 1)
                {
                    path = solution.Dequeue();
                    index--;
                }
                for (int i = 0; i < path.Count; i++)
                {
                    room[path[i].Item1, path[i].Item2] = mainPath4[i];
                }
                break;
        }
    }

    private Queue<List<(int, int)>> MainPathBFSHelper(Vector2Int entrance, int length)
    {
        Queue<List<(int, int)>> solution = new Queue<List<(int, int)>>();

        List<(int, int)> current = new List<(int, int)>();
        current.Add((entrance.x, entrance.y));
        solution.Enqueue(current);

        while (solution.Peek().Count + 1 <= length)
        {
            int x = current[current.Count - 1].Item1;
            int y = current[current.Count - 1].Item2;

            if (isValidMove(x - 1, y) && !current.Contains((x - 1, y)))
            {
                List<(int, int)> path = new List<(int, int)>();
                foreach((int, int) position in current)
                {
                    path.Add(position);
                }
                path.Add((x - 1, y));
                solution.Enqueue(path);
            }

            if (isValidMove(x + 1, y) && !current.Contains((x + 1, y)))
            {
                List<(int, int)> path = new List<(int, int)>();
                foreach ((int, int) position in current)
                {
                    path.Add(position);
                }
                path.Add((x + 1, y));
                solution.Enqueue(path);
            }

            if (isValidMove(x, y - 1) && !current.Contains((x, y - 1)))
            {
                List<(int, int)> path = new List<(int, int)>();
                foreach ((int, int) position in current)
                {
                    path.Add(position);
                }
                path.Add((x, y - 1));
                solution.Enqueue(path);
            }

            if (isValidMove(x, y + 1) && !current.Contains((x, y + 1)))
            {
                List<(int, int)> path = new List<(int, int)>();
                foreach ((int, int) position in current)
                {
                    path.Add(position);
                }
                path.Add((x, y + 1));
                solution.Enqueue(path);
            }

            if (solution.Peek().Count + 1 <= length) {
                current = solution.Dequeue();
            }
        }

        return solution;
    }

    private void FindPath(Vector2Int source) 
    {
        List<Vector2Int> path = Utility.FindPath(size, source, isValidMove, (int x, int y) => { return room[x, y] > 0; });

        foreach (Vector2Int position in path)
        {
            room[position.x,position.y] = 2;
        }

        room[source.x,source.y] = 8;
    }

    private void FindPath(int type)
    {

    }
}
