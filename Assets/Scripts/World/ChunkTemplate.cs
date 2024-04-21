using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChunkTemplate
{
    public int type;
    public Wrapper<Wrapper<int>> layout;
}
