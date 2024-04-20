using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    private int type;
    private ChunkTemplate template;

    public Chunk() {
        type = 0;
    }

    public void SetType(int type) {
        this.type = type;
    }

    public void Initialize() {
        template = ChunkTemplateManager.Instance.GetRandomChunkTemplate(type);
    }
}
