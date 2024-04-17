using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    void Start()
    {
        speed = 10f;
        stateMachine.Initialize(idleState);
    }
}
