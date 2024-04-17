using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public delegate bool isValidMoveDelegate(int x, int y, bool inclusive = false);
    public delegate bool isDestinationDelegate(int x, int y);

    public static List<Vector2Int> FindPath(int size, Vector2Int source, isValidMoveDelegate isValidMove, isDestinationDelegate isDestination)
    {
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
            if (isDestination(cur.x, cur.y)) {
                break;
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
}
