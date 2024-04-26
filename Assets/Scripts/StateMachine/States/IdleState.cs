public class IdleState : State
{
    Movement movement;

    public IdleState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        movement = entity.movement;
    }

    public override void Enter()
    {
        base.Enter();

        movement.SetVelocityX(0);
    }
}
