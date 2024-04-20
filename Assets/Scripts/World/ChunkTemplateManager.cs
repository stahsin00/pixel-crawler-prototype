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
}
