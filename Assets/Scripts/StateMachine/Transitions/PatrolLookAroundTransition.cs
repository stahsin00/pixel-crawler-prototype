using UnityEngine;

public class PatrolLookAroundTransition : SubStateTransiton
{
    public PatrolLookAroundTransition(Entity entity, SuperState superState) : base(entity, superState)
    {
    }

    public override State NextState()
    {
        PatrolState patrolState = superState as PatrolState;
        return patrolState.lookAroundState;
    }

    public override bool ShouldTransition()
    {
        PatrolState patrolState = superState as PatrolState;
        return Time.time - patrolState.subStateMachine.CurrentState.startTime > 1.5f;
    }
}
