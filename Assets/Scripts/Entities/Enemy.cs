using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    void Start()
    {
        speed = 5f;
        stateMachine.Initialize(idleState);
    }
}
