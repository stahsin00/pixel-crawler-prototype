using UnityEngine;

public class PlayerFinishAttackTransition : Transition
{
    public PlayerFinishAttackTransition(Entity entity) : base(entity)
    {
    }

    public override State NextState()
    {
        return entity.idleState;
    }

    public override bool ShouldTransition()
    {
        return Time.time - entity.stateMachine.CurrentState.startTime >= 0.1f;
    }
}
