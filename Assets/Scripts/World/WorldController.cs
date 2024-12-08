using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using PixelCrawler.World;

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
        //RenderRoom();
        Render();
    }

    // TODO: Temp
    private void Render() {

        for (int i = 0; i < CurrentWorld.Size; i++) {
            for (int j = 0; j < CurrentWorld.Size; j++) {
                if (CurrentWorld.RegionMap[i,j] > 0) {
                    Region CurrentRegion = CurrentWorld.GetRegion(CurrentWorld.RegionMap[i,j]);
                    for (int k = 0; k < CurrentRegion.Size; k++) {
                        for (int l = 0; l < CurrentRegion.Size; l++) {
                            if (CurrentRegion.RoomMap[k,l] > 0) {
                                Room CurrentRoom = CurrentRegion.GetRoom(k,l);
                                for (int x = 0; x < CurrentRoom.size * CurrentRoom.chunkSize; x++)
                                {
                                    for (int y = 0; y < CurrentRoom.size * CurrentRoom.chunkSize; y++)
                                    {
                                        if (CurrentRoom[x,y] == 1) {
                                            Vector3Int tilePosition = new Vector3Int((i * CurrentRegion.Size * CurrentRoom.size * 5) + (k * CurrentRoom.size * 5) + x,// - (i*CurrentRoom.size) - k, 
                                                                                     (j * CurrentRegion.Size * CurrentRoom.size * 5) + (l * CurrentRoom.size * 5) + y,// - (j*CurrentRoom.size) - l, 
                                                                                      0);
                                            tilemap.SetTile(tilePosition, tile);
                                            TileBase tileGO = tilemap.GetTile(tilePosition);
                                            if (tileGO == null) Debug.Log("wtf");
                                            tilemap.SetTileFlags(tilePosition, TileFlags.None);

                                            int region = CurrentRegion.RegionType;
                                            switch (region) {
                                                case 1:
                                                    tilemap.SetColor(tilePosition, Color.green);
                                                    break;
                                                case 2:
                                                    tilemap.SetColor(tilePosition, Color.blue);
                                                    break;
                                                case 3:
                                                    tilemap.SetColor(tilePosition, Color.red);
                                                    break;
                                                default:
                                                    tilemap.SetColor(tilePosition, Color.yellow);
                                                    break;
                                            }
                                        } else if (CurrentRoom[x,y] == 2) {
                                            playerInstance = Instantiate(player, 
                                                                         new Vector3((i * CurrentRegion.Size * CurrentRoom.size * 5) + (k * CurrentRoom.size * 5) + x + tilemap.cellSize.x / 2 - (i*CurrentRoom.size) - k, 
                                                                                     (j * CurrentRegion.Size * CurrentRoom.size * 5) + (l * CurrentRoom.size * 5) + y + tilemap.cellSize.y / 2 - (j*CurrentRoom.size) - l, 0), 
                                                                                      Quaternion.identity);
                                        } else if (CurrentRoom[x,y] == 3) {
                                            enemyInstances.Add(Instantiate(enemy, 
                                                                           new Vector3((i * CurrentRegion.Size * CurrentRoom.size * 5) + (k * CurrentRoom.size * 5) + x + tilemap.cellSize.x / 2 - (i*CurrentRoom.size) - k, 
                                                                                       (j * CurrentRegion.Size * CurrentRoom.size * 5) + (l * CurrentRoom.size * 5) + y + tilemap.cellSize.y / 2 - (j*CurrentRoom.size) - l, 0), 
                                                                                        Quaternion.identity));
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }

        // TODO: temp
        isLoading = false;
    }

    private void Update() {
        if (!isLoading && !isChangingRoom && (playerInstance.transform.position.x < 0 || playerInstance.transform.position.x > CurrentRoom.size * CurrentRoom.chunkSize - 1 || playerInstance.transform.position.y < 0 || playerInstance.transform.position.y > CurrentRoom.size * CurrentRoom.chunkSize - 1)) {
            isChangingRoom = true;
            //TriggerRoomChange();
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

        Debug.Log($"Region: ({CurrentRoom.RegionX},{CurrentRoom.RegionY}); World: ({CurrentRoom.RoomRegion.WorldX},{CurrentRoom.RoomRegion.WorldY})");

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

        Debug.Log($"Region: ({CurrentRoom.RegionX},{CurrentRoom.RegionY}); World: ({CurrentRoom.RoomRegion.WorldX},{CurrentRoom.RoomRegion.WorldY})");
        isChangingRoom = false;
    }
}
