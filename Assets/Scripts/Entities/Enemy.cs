public class Enemy : Entity
{
    public AISenses aISenses;

    public PatrolState patrolState;

    protected override void Awake()
    {
        base.Awake();

        aISenses = GetComponent<AISenses>();


        patrolState = new PatrolState(this, stateMachine);

        idleState.AddTransition(new EnemyIdleTransition(this));
        patrolState.AddTransition(new EnemyPatrolTransition(this));
    }

    void Start()
    {
        speed = 5f;
        stateMachine.Initialize(idleState);
    }
}
