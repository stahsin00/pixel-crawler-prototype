public class EntityFallTransition : Transition
{
    public EntityFallTransition(Entity entity) : base(entity)
    {
    }

    public override State NextState()
    {
        return entity.inAirState;
    }

    public override bool ShouldTransition()
    {
        return !entity.collisionSenses.IsGrounded() && entity.movement.currentVelocity.y < 0.01f && entity.stateMachine.CurrentState != entity.inAirState;
    }
}
