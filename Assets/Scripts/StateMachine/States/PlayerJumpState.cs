using UnityEngine;

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

    public override void Update()
    {
        base.Update();

        Player player = entity as Player;
        if (Mathf.Abs(player.inputHandler.InputX) > 0.1f) {
            player.movement.SetVelocityX(player.speed);
        } else {
            player.movement.SetVelocityX(0);
        }
    }
}
