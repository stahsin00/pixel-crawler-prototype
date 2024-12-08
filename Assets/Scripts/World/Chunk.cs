using UnityEngine;

namespace PixelCrawler.World
{
    public class Chunk
    {
        private int type;
        private int size;
        private ChunkTemplate template;

        private Room room;

        private bool isSpawn;
        private Vector2Int spawnPoint;
        private Vector2Int enemySpawnPoint;

        private int posX;
        private int posY;

        public int[,] Layout { get; private set; }

        public Chunk(Room room, int size, int x, int y, bool spawn = false, int type = 0) {
            this.room = room;
            this.type = type;
            this.size = size;

            posX = x;
            posY = y;

            isSpawn = spawn;
            spawnPoint = new Vector2Int(size,size);
            enemySpawnPoint = new Vector2Int(size,size);
            
            Layout = new int[size, size];
        }

        public void SetSpawn() {
            isSpawn = true;
        }

        public void SetType(int type) {
            this.type = type;
        }

        public void Initialize() {
            int tempType = 1;


            if (posY == 0) {
                Region region = room.RoomRegion;
                if (room.RegionY == 0) {
                    World world = region.World;
                    if (region.WorldY == 0 || world.RegionMap[region.WorldX, region.WorldY - 1] == 0) {

                    } else {
                        Region neighborRegion = world.GetRegion(world.RegionMap[region.WorldX, region.WorldY - 1]);
                        try {
                            if (neighborRegion.GetRoom(room.RegionX, region.Size-1).chunkMap[posX, room.size-1] > 0) tempType = 3;
                        } catch {

                        }
                    }
                } else  {
                    try {
                        if (region.Map[room.RegionX, room.RegionY-1].chunkMap[posX,room.size-1] > 0) {
                            tempType = 3;
                        }
                    } catch {
                    }
                }
            } else if (room.chunkMap[posX, posY - 1] > 0) {
                tempType = 3;
            }

            if (posY == room.size-1) {
                Region region = room.RoomRegion;
                if (room.RegionY == region.Size-1) {
                    World world = region.World;
                    if (region.WorldY == world.Size-1 || world.RegionMap[region.WorldX, region.WorldY + 1] == 0) {

                    } else {
                        Region neighborRegion = world.GetRegion(world.RegionMap[region.WorldX, region.WorldY + 1]);
                        try {
                            if (neighborRegion.GetRoom(room.RegionX, 0).chunkMap[posX, 0] > 0) tempType = (tempType == 3) ? 4 : 2;
                        } catch {

                        }
                    }
                } else {
                    try {
                        if (region.GetRoom(room.RegionX, room.RegionY+1).chunkMap[posX,0] > 0)
                            tempType = (tempType == 3) ? 4 : 2;
                    } catch {

                    }
                }
            } else if (room.chunkMap[posX, posY + 1] > 0) {
                tempType = (tempType == 3) ? 4 : 2;
            }


            template = WorldController.Instance.TemplateManager.GetRandomChunkTemplate(tempType);
            //template = WorldController.Instance.TemplateManager.GetRandomChunkTemplate(type);
            //template = WorldController.Instance.TemplateManager.GetRandomChunkTemplate(4);

            // TODO
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    Layout[i, j] = template.layout.items[j].items[i];
                    if (Layout[i, j] == 0 && isSpawn && spawnPoint.x >= size && spawnPoint.y >= size) {
                        Layout[i, j] = 2;
                        spawnPoint.x = i;
                        spawnPoint.y = j;
                    } else if (Layout[i, j] == 0 && isSpawn && enemySpawnPoint.x >= size && enemySpawnPoint.y >= size && Random.value <= 0.5 && i > 0) {
                        Layout[i, j] = 3;
                        enemySpawnPoint.x = i;
                        enemySpawnPoint.y = j;
                    }
                }
            }
        }

        public int this[int row, int col] {
            get { return Layout[row, col]; }
        }
    }
}