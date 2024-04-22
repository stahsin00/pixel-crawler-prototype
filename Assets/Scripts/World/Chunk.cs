using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    private int type;
    private int size;
    private ChunkTemplate template;

    public int[,] Layout { get; private set; }

    public Chunk(int size) {
        type = 0;
        this.size = size;
        
        Layout = new int[size, size];
    }

    public void SetType(int type) {
        this.type = type;
    }

    public void Initialize() {
        template = WorldController.Instance.TemplateManager.GetRandomChunkTemplate(type);

        // TODO
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                Layout[i, j] = template.layout.items[i].items[j];
            }
        }
    }

    public int this[int row, int col] {
        get { return Layout[row, col]; }
    }
}
