using UnityEngine;

public class PatrolMarchTransition : SubStateTransiton
{
    public PatrolMarchTransition(Entity entity, SuperState superState) : base(entity, superState)
    {
    }

    public override State NextState()
    {
        PatrolState patrolState = superState as PatrolState;
        return patrolState.marchState;
    }

    public override bool ShouldTransition()
    {
        PatrolState patrolState = superState as PatrolState;
        return Time.time - patrolState.subStateMachine.CurrentState.startTime > 1f;
    }
}
