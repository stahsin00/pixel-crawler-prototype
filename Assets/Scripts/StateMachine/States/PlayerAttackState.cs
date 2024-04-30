using UnityEngine;

public class PlayerAttackState : AttackState
{
    public PlayerAttackState(Entity entity, StateMachine stateMachine, GameObject attackPos) : base(entity, stateMachine, attackPos)
    {
    }

    public override void Enter()
	{
		base.Enter();

		Player player = entity as Player;
        player.inputHandler.UseAttackInput();
	}
}
