public class WalkState : State
{
    Movement movement;

    public WalkState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        movement = entity.movement;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        movement.SetVelocityX(entity.speed);
    }
}
