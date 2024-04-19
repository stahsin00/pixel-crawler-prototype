using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public int Size { get; private set; }
    public int RegionSize { get; private set; }

    public int[,] RegionMap { get; private set; }
    public Region[] Regions { get; private set; }

    // TODO
    private float fillerChance = 0.75f;
    private int maxFillerCount = 2;

    public World()
    {
        Size = 4;
        RegionSize = 4;

        RegionMap = new int[Size, Size];
        Regions = new Region[5];

        Initialize();
    }

    private void Initialize() {
        
        PlaceRegions();
        MakeConnections();
        InitializeRegions();

    }

    private void SetRegion(int x, int y, Region region) {
        RegionMap[x, y] = region.type;
        Regions[region.type-1] = region;
    }

    // TODO
    private void PlaceRegions() {
        int x = 0;
        int y = Random.Range(0, 4);

        SetRegion(x,y,new Region(1,x,y,true));

        PlaceRegion(2, ref x, ref y);
        PlaceRegion(3, ref x, ref y);

        int roomNum = 3;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                
                if (RegionMap[i,j] != 1 && RegionMap[i, j] != 2 && RegionMap[i, j] != 3)
                {
                    
                    if (((i != 0 && RegionMap[i-1,j] > 0) || (i != 3 && RegionMap[i + 1, j] > 0) || (j != 0 && RegionMap[i, j - 1] > 0) || (j != 3 && RegionMap[i, j + 1] > 0)) && maxFillerCount > 0 && Random.value < fillerChance)
                    {
                        SetRegion(i,j,new Region(roomNum+1,i,j));
                        roomNum++;
                        maxFillerCount--;
                    } 
                } 
            }
        }
    }

    private void PlaceRegion(int roomNum, ref int x, ref int y) {
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

        if ((tempX < 0) || (tempY < 0) || (tempX >= Size) || (tempY >= Size) || (RegionMap[tempX,tempY] != 0)) {
            PlaceRegion(roomNum, ref x, ref y);
        } else {
            x = tempX;
            y = tempY;

            SetRegion(x,y,new Region(roomNum,x,y));
        }
    }

    private void MakeConnections() {
        for (int i = 0; i < Regions.Length; i++) {
            for (int j = i + 1; j < Regions.Length; j++) {
                Region region1 = Regions[i];
                Region region2 = Regions[j];
                if (region1 == null || region2 == null) {
                    break;
                }
                if (region1.worldX == region2.worldX) {
                    int row = Random.Range(0, RegionSize);
                    int col = 0;

                    if (region1.worldY < region2.worldY) {
                        col = RegionSize-1;
                    }

                    Regions[i].AddExit(new Vector2Int(row, col));
                    Regions[j].AddEntrance(new Vector2Int(row, RegionSize-1 - col));
                } else if (region1.worldY == region2.worldY) {
                    int col = Random.Range(0, RegionSize);
                    int row = 0;

                    if (region1.worldX < region2.worldX) {
                        row = RegionSize-1;
                    }

                    Regions[i].AddExit(new Vector2Int(row, col));
                    Regions[j].AddEntrance(new Vector2Int(RegionSize-1 - row, col));
                }
            }
        }
    }

    private void InitializeRegions() {
        for (int i = 0; i < 5; i++) {
            if (Regions[i] != null) {
                Regions[i].Initialize();
            }
        }
    }
}
