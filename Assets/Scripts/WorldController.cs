using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController
{
    private static WorldController instance;
    public World CurrentWorld { get; private set; }

    private WorldController() 
    {
        CurrentWorld = new World();
    }

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
}
