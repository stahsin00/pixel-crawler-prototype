using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldController : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile tile;

    public GameObject player;

    private static WorldController instance;
    public static WorldController Instance { get { return instance; } }

    public World CurrentWorld { get; private set; }
    public Room CurrentRoom { get; private set; }

    public ChunkTemplateManager TemplateManager { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            CurrentWorld = new World();

            TemplateManager = new ChunkTemplateManager();
            StartCoroutine(WaitForTemplates());
        }
    }

    private void RenderRoom() {
        for (int x = 0; x < CurrentRoom.size * CurrentRoom.chunkSize - 1; x++)
        {
            for (int y = 0; y < CurrentRoom.size * CurrentRoom.chunkSize - 1; y++)
            {
                if (CurrentRoom[x,y] == 1) {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                } else if (CurrentRoom[x,y] == 2) {
                    Instantiate(player, new Vector3(x + tilemap.cellSize.x / 2, y + tilemap.cellSize.y / 2, 0), Quaternion.identity);
                }
            }
        }
    }

    IEnumerator WaitForTemplates() {
        while (TemplateManager.isLoading) {
            yield return null;
        }

        // TODO: temp
        CurrentRoom = new Room();
        RenderRoom();
    }
}
