using System.Collections.Generic;
using UnityEngine;

public class ChunkTemplateManager : MonoBehaviour
{
    private static ChunkTemplateManager instance;
    public static ChunkTemplateManager Instance { get { return instance; } }

    public List<ChunkTemplate> rightLeft;
    public List<ChunkTemplate> rightLeftBottom;
    public List<ChunkTemplate> rightLeftTop;
    public List<ChunkTemplate> allSides;

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
            LoadChunkTemplates();
            DontDestroyOnLoad(gameObject);
        }
    }

    public ChunkTemplate GetRandomChunkTemplate(int type)
    {
        int index;
        switch (type) {
            case 1:
                index = Random.Range(0, rightLeft.Count);
                return rightLeft[index];
            case 2:
                index = Random.Range(0, rightLeftBottom.Count);
                return rightLeftBottom[index];
            case 3:
                index = Random.Range(0, rightLeftTop.Count);
                return rightLeftTop[index];
            default:
                index = Random.Range(0, allSides.Count);
                return allSides[index];
        }

    }

    private void LoadChunkTemplates()
    {
        string json = Resources.Load<TextAsset>("templates").text;
        ChunkTemplate[] data = JsonUtility.FromJson<Wrapper<ChunkTemplate>>(json).items;

        foreach (ChunkTemplate chunkTemplate in data) {
            int type = chunkTemplate.type;

            switch (type) {
                case 1:
                    rightLeft.Add(chunkTemplate);
                    break;
                case 2:
                    rightLeftBottom.Add(chunkTemplate);
                    break;
                case 3:
                    rightLeftTop.Add(chunkTemplate);
                    break;
                default:
                    allSides.Add(chunkTemplate);
                    break;
            }
        }
    }
}
