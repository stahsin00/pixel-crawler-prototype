public class EntityLandTransition : Transition
{
    public EntityLandTransition(Entity entity) : base(entity)
    {
    }

    public override State NextState()
    {
        return entity.idleState;
    }

    public override bool ShouldTransition()
    {
        return entity.collisionSenses.IsGrounded() && entity.movement.currentVelocity.y < 0.01f && entity.stateMachine.CurrentState == entity.inAirState;
    }
}
