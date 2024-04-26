public class PlayerJumpTransition : Transition
{
    public PlayerJumpTransition(Entity entity) : base(entity)
    {
    }

    public override State NextState()
    {
        Player player = entity as Player;
        return player.jumpState;
    }

    public override bool ShouldTransition()
    {
        Player player = entity as Player;
        return player.inputHandler.JumpInput && player.stateMachine.CurrentState != player.jumpState;
    }
}
