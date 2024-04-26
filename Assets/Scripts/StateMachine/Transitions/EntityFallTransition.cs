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
        if (entity.stateMachine.CurrentState == entity.inAirState) {
            return false;
        }

        if (!entity.collisionSenses.IsGrounded() && (entity.movement.currentVelocity.y < 0.01f || entity.collisionSenses.HasCollided)) {
            return true;
        }

        return false;
    }
}
