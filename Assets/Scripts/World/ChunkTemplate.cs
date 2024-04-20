using System.Collections.Generic;
using UnityEngine;

// TODO T-T
[CreateAssetMenu(fileName = "New Chunk Template", menuName = "Chunk Template")]
public class ChunkTemplate : ScriptableObject
{
    public int type;

    // no.
    public int[] row1 = new int[8];
    public int[] row2 = new int[8];
    public int[] row3 = new int[8];
    public int[] row4 = new int[8];
    public int[] row5 = new int[8];
    public int[] row6 = new int[8];
    public int[] row7 = new int[8];
    public int[] row8 = new int[8];
}
