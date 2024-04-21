using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    private int type;
    private int size = 5;
    private ChunkTemplate template;

    public int[,] Layout { get; private set; }

    public Chunk() {
        type = 0;
        
        Layout = new int[size, size];
    }

    public void SetType(int type) {
        this.type = type;
    }

    public void Initialize() {
        template = ChunkTemplateManager.Instance.GetRandomChunkTemplate(type);

        // TODO
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                Layout[i, j] = template.layout[i][j];
            }
        }
    }
}
