using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public int width = 4;
    public int regionSize = 4;
    private float fillerChance = 0.75f;
    private int fillerCount = 2;

    public int[,] region;  // why
    private Region[] regions;

    private int x;
    private int y;

    public World()
    {
        region = new int[width, width];
        regions = new Region[5];

        x = 0;
        y = Random.Range(0, 4);

        region[x, y] = 1;
        regions[0] = new Region(x,y,true);

        PlaceRegion(2);
        PlaceRegion(3);

        int roomNum = 3;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                // wtf?
                if (region[i,j] != 1 && region[i, j] != 2 && region[i, j] != 3)
                {
                    // lol
                    if (((i != 0 && region[i-1,j] > 0) || (i != 3 && region[i + 1, j] > 0) || (j != 0 && region[i, j - 1] > 0) || (j != 3 && region[i, j + 1] > 0)) && fillerCount > 0 && Random.value < fillerChance)
                    {
                        region[i, j] = 4;
                        Region roomX = new Region(i,j);
                        regions[roomNum] = roomX;
                        roomNum++;
                        fillerCount--;
                    } 
                } 
            }
        }

        MakeConnections();

        for (int i = 0; i < 5; i++) {
            if (regions[i] != null) {
                regions[i].Initialize();
            }
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
                    int row = Random.Range(0, regionSize);
                    int col = 0;

                    if (region1.worldY < region2.worldY) {
                        col = regionSize-1;
                    }

                    regions[i].AddExit(new Vector2Int(row, col));
                    regions[j].AddEntrance(new Vector2Int(row, regionSize-1 - col));
                } else if (region1.worldY == region2.worldY) {
                    int col = Random.Range(0, regionSize);
                    int row = 0;

                    if (region1.worldX < region2.worldX) {
                        row = regionSize-1;
                    }

                    regions[i].AddExit(new Vector2Int(row, col));
                    regions[j].AddEntrance(new Vector2Int(regionSize-1 - row, col));
                }
            }
        }
    }

    private void PlaceRegion(int roomNum) {
        int tempX = x;
        int tempY = y;

        int direction = Random.Range(0, 4);

        switch (direction)
        {
            case 0:
                tempY++;
                break;
            case 1:
                tempX++;
                break;
            case 2:
                tempY--;
                break;
            default:
                tempX--;
                break;
        }

        if ((tempX < 0) || (tempY < 0) || (tempX >= width) || (tempY >= width) || (region[tempX,tempY] != 0)) {
            PlaceRegion(roomNum);
        } else {
            x = tempX;
            y = tempY;

            Region room1 = new Region(x,y);
            regions[roomNum - 1] = room1;
            region[x, y] = roomNum;
        }
    }
}
