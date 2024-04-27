using UnityEngine;

public class PatrolMarchTransition : Transition
{
    public PatrolMarchTransition(Entity entity) : base(entity)
    {
    }

    public override State NextState()
    {
        PatrolState superState = entity.stateMachine.CurrentState as PatrolState;
        return superState.marchState;
    }

    public override bool ShouldTransition()
    {
        PatrolState superState = entity.stateMachine.CurrentState as PatrolState;
        return Time.time - superState.subStateMachine.CurrentState.startTime > 1f;
    }
}
