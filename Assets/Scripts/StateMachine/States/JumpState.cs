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
}
