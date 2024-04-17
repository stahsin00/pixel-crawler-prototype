using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    Movement movement;
	float speed = 15f;

    public JumpState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
		movement = entity.movement;
	}

	public override void Enter()
	{
		base.Enter();

		movement.SetVelocityY(speed);
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
