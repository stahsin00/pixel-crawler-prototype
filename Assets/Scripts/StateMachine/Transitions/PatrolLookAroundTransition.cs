using UnityEngine;

public class PatrolLookAroundTransition : Transition
{
    public PatrolLookAroundTransition(Entity entity) : base(entity)
    {
    }

    public override State NextState()
    {
        PatrolState superState = entity.stateMachine.CurrentState as PatrolState;
        return superState.lookAroundState;
    }

    public override bool ShouldTransition()
    {
        PatrolState superState = entity.stateMachine.CurrentState as PatrolState;
        return Time.time - superState.subStateMachine.CurrentState.startTime > 1.5f;
    }
}
