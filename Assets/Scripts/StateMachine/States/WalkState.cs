using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    Movement movement;

    public WalkState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        movement = entity.movement;
    }

    public override void Update()
    {
        base.Update();

        if (movement.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(entity.inAirState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        movement.SetVelocityX(entity.speed);
    }
}
