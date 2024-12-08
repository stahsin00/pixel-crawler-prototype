using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrawler.World
{
    public abstract class Grid<T>
    {
        public int Size { get; set; }
        public int CellSize { get; set; }
        public int[,] Map { get; set; }
        public T[,] CellMap { get; set; }

        public abstract void MakePath();
        public abstract void Initialize();
    }
}
