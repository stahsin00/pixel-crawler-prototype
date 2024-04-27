public class EnemyPatrolTransition : Transition
{
    public EnemyPatrolTransition(Entity entity) : base(entity)
    {
    }

    public override State NextState()
    {
        return entity.idleState;
    }

    public override bool ShouldTransition()
    {
        Enemy enemy = entity as Enemy;
        return enemy.aISenses.DetectsObstacle();
    }
}
