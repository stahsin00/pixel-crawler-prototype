using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : State
{
    Movement movement;
    public InAirState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        movement = entity.movement;
    }

    public override void Update()
    {
        base.Update();

        if (entity.collisionSenses.IsGrounded() && movement.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(entity.idleState);
        }
    }
}
