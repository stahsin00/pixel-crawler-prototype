using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController
{
    private static WorldController instance;
    public World CurrentWorld { get; private set; }
    public Room CurrentRoom { get; private set; }

    public static WorldController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WorldController();
            }
            return instance;
        }
    }

    private WorldController() 
    {
        CurrentWorld = new World();

        // TODO: temp
        CurrentRoom = new Room();
        RenderRoom();
    }

    private void RenderRoom() {

    }
}
