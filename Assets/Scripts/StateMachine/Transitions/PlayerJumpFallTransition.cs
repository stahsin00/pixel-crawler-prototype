using UnityEngine;

public class PlayerJumpFallTransition : Transition
{
    public PlayerJumpFallTransition(Entity entity) : base(entity)
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

        if (entity.collisionSenses.HasCollided && Time.time - entity.stateMachine.CurrentState.startTime > 0.1f) {
            return true;
        }

        return false;
    }
}
