using UnityEngine;

public class PlayerMoveTransition : Transition
{
    public PlayerMoveTransition(Entity entity) : base(entity)
    {
    }

    public override State NextState()
    {
        Player player = entity as Player;
        return player.idleState;
    }

    public override bool ShouldTransition()
    {
        Player player = entity as Player;
        return Mathf.Abs(player.inputHandler.InputX)  <= 0.1f && player.stateMachine.CurrentState != player.idleState;
    }
}
