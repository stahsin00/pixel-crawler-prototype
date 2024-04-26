using UnityEngine;

public class PlayerIdleTransition : Transition
{
    public PlayerIdleTransition(Player player) : base(player)
    {
    }

    public override bool ShouldTransition()
    {
        Player player = entity as Player;
        return Mathf.Abs(player.inputHandler.InputX)  > 0.1f && player.stateMachine.CurrentState != player.walkState;
    }

    public override State NextState()
    {
        Player player = entity as Player;
        return player.walkState;
    }
}
