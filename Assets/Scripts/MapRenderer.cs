using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MapRenderer : MonoBehaviour
{
    public RawImage image;
    private World world;

    
    void Start()
    {
        world = WorldController.Instance.CurrentWorld;
        DisplayMap();
    }

    // temp
    void DisplayMap()
    {
        int cellSize = 10;
        int size = world.Size * world.RegionSize * cellSize;

        Texture2D texture = new Texture2D(size, size);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i % cellSize == 0 || j % cellSize == 0) {
                    texture.SetPixel(i, j, Color.black);
                    continue;
                }

                int region = world.RegionMap[i / (world.RegionSize * cellSize), j / (world.RegionSize * cellSize)];

                if (region == 0) {
                    texture.SetPixel(i, j, Color.black);
                    continue;
                }

                int room = world.GetRegion(region).RoomMap[i / cellSize % world.RegionSize, 
                                                           j / cellSize % world.RegionSize];

                if (room == 0) {
                    texture.SetPixel(i, j, Color.black);
                    continue;
                }

                switch (region) {
                    case 1:
                        texture.SetPixel(i, j, Color.green);
                        break;
                    case 2:
                        texture.SetPixel(i, j, Color.blue);
                        break;
                    case 3:
                        texture.SetPixel(i, j, Color.red);
                        break;
                    default:
                        texture.SetPixel(i, j, Color.yellow);
                        break;
                }
            }
        }

        texture.Apply();
        image.texture = texture;

        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/TempPreview.png", bytes);
    }
}
