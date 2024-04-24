using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Entity entity;
    protected StateMachine stateMachine;
    protected Transition transition;

    public State(Entity entity, StateMachine stateMachine)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Update()
    {
        if (transition.ShouldTransition()) {
            stateMachine.ChangeState(transition.NextState());
        }
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
