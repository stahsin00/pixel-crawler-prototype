using UnityEngine;

public class MarchState : State
{
    Movement movement;

    private float marchSpeed = 2f;

    public MarchState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        movement = entity.movement;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        movement.SetVelocityX(marchSpeed);
    }
}
