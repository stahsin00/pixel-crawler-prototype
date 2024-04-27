public class LookAroundState : State
{
    Movement movement;

    public LookAroundState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        movement = entity.movement;
    }

    public override void Exit() {
        movement.Flip();
    }
}
