using UnityEngine;

public class EnemyIdleTransition : Transition
{
    public EnemyIdleTransition(Entity entity) : base(entity)
    {
    }

    public override State NextState()
    {
        Enemy enemy = entity as Enemy;
        return enemy.patrolState;
    }

    public override bool ShouldTransition()
    {
        Enemy enemy = entity as Enemy;
        return Time.time - enemy.stateMachine.CurrentState.startTime > 1f && enemy.stateMachine.CurrentState != enemy.patrolState;
    }
}
