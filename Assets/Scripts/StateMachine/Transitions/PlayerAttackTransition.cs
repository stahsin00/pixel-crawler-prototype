public class PlayerAttackTransition : Transition
{
    public PlayerAttackTransition(Entity entity) : base(entity)
    {
    }

    public override State NextState()
    {
        Player player = entity as Player;
        return player.attackState;
    }

    public override bool ShouldTransition()
    {
        Player player = entity as Player;
        return player.inputHandler.AttackInput && player.stateMachine.CurrentState != player.attackState;
    }
}
