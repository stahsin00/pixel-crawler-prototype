public class PlayerJumpState : JumpState
{
    public PlayerJumpState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
    }

    public override void Enter()
	{
		base.Enter();

		Player player = entity as Player;
        player.inputHandler.UseJumpInput();
	}
}
