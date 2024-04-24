using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition
{
    protected Entity entity;
    protected StateMachine stateMachine;

    public Transition(Entity entity, StateMachine stateMachine)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
    }

    public virtual bool ShouldTransition()
    {
        // TODO : temp
        return false;
    }

    public virtual State NextState()
    {
        // TODO : temp
        return entity.idleState;
    }
}
