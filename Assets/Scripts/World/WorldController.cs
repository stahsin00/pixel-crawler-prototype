using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldController : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile tile;

    public GameObject player;
    private GameObject playerInstance;

    public GameObject enemy;
    private List<GameObject> enemyInstances = new List<GameObject>();

    private static WorldController instance;
    public static WorldController Instance { get { return instance; } }

    public World CurrentWorld { get; private set; }
    public Room CurrentRoom { get; private set; }

    public ChunkTemplateManager TemplateManager { get; private set; }

    public bool isLoading {get; private set; } = true;
    private bool isChangingRoom = false;

    public GameObject GetPlayer() {
        return playerInstance;
    }

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
                    playerInstance = Instantiate(player, new Vector3(x + tilemap.cellSize.x / 2, y + tilemap.cellSize.y / 2, 0), Quaternion.identity);
                } else if (CurrentRoom[x,y] == 3) {
                    enemyInstances.Add(Instantiate(enemy, new Vector3(x + tilemap.cellSize.x / 2, y + tilemap.cellSize.y / 2, 0), Quaternion.identity));
                }
            }

            //tilemap.SetTile(new Vector3Int(x, -1, 0), tile);
            //tilemap.SetTile(new Vector3Int(x, CurrentRoom.size * CurrentRoom.chunkSize - 1, 0), tile);

            //tilemap.SetTile(new Vector3Int(-1, x, 0), tile);
            //tilemap.SetTile(new Vector3Int(CurrentRoom.size * CurrentRoom.chunkSize - 1, x, 0), tile);
        }

        // TODO: temp
        isLoading = false;
    }

    IEnumerator WaitForTemplates() {
        while (TemplateManager.isLoading) {
            yield return null;
        }

        // TODO: temp
        CurrentWorld = new World();
        CurrentRoom = CurrentWorld.GetSpawn();
        RenderRoom();
    }

    private void Update() {
        if (!isChangingRoom && (playerInstance.transform.position.x < 0 || playerInstance.transform.position.x > CurrentRoom.size * CurrentRoom.chunkSize - 1 || playerInstance.transform.position.y < 0 || playerInstance.transform.position.y > CurrentRoom.size * CurrentRoom.chunkSize - 1)) {
            Debug.Log("change room");
            isChangingRoom = true;
            TriggerRoomChange();
        }
    }

    private void TriggerRoomChange() {
        List<GameObject> enemiesToRemove = new List<GameObject>();

        foreach (GameObject enemyInstance in enemyInstances) {
            enemiesToRemove.Add(enemyInstance);
        }

        foreach (GameObject enemyInstance in enemiesToRemove) {
            enemyInstances.Remove(enemyInstance);
            Destroy(enemyInstance);
        }

        // TODO: will fix later
        if (playerInstance.transform.position.x < 0) {
            CurrentRoom = CurrentWorld.GetAdjacentRoom(CurrentRoom,-1,0);
            RenderRoom();
            playerInstance.transform.position = new Vector2(CurrentRoom.size * CurrentRoom.chunkSize - 1, playerInstance.transform.position.y);
        } else if (playerInstance.transform.position.x > CurrentRoom.size * CurrentRoom.chunkSize - 1) {
            CurrentRoom = CurrentWorld.GetAdjacentRoom(CurrentRoom,1,0);
            RenderRoom();
            playerInstance.transform.position = new Vector2(0, playerInstance.transform.position.y);
        } else if (playerInstance.transform.position.y < 0) {
            CurrentRoom = CurrentWorld.GetAdjacentRoom(CurrentRoom,0,-1);
            RenderRoom();
            playerInstance.transform.position = new Vector2(playerInstance.transform.position.x, CurrentRoom.size * CurrentRoom.chunkSize - 1);
        } else {
            CurrentRoom = CurrentWorld.GetAdjacentRoom(CurrentRoom,0,1);
            RenderRoom();
            playerInstance.transform.position = new Vector2(playerInstance.transform.position.x, 0);
        }

        Debug.Log($"player position: ({playerInstance.transform.position.x},{playerInstance.transform.position.y})");
        isChangingRoom = false;
    }
}
