using UnityEngine;

public class Chunk
{
    private int type;
    private int size;
    private ChunkTemplate template;

    private bool isSpawn;
    private Vector2Int spawnPoint;
    private Vector2Int enemySpawnPoint;

    public int[,] Layout { get; private set; }

    public Chunk(int size, bool spawn = false, int type = 0) {
        this.type = type;
        this.size = size;

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
        template = WorldController.Instance.TemplateManager.GetRandomChunkTemplate(type);

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
