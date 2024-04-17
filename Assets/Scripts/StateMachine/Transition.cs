using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    protected Entity entity;
    protected StateMachine stateMachine;

    public virtual bool ShouldTransition()
    {
        return false;
    }

    public virtual State NextState()
    {
        return entity.idleState;
    }
}
