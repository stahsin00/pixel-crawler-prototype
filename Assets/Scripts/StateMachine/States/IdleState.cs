using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    Movement movement;

    public IdleState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        movement = entity.movement;
    }

    public override void Enter()
    {
        base.Enter();

        movement.SetVelocityX(0);
    }

    public override void Update()
    {
        base.Update();

        if (movement.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(entity.inAirState);
        }
    }
}
