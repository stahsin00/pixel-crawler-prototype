public class Player : Entity
{
    public InputHandler inputHandler;

    protected override void Awake()
    {
        base.Awake();

        inputHandler = GetComponent<InputHandler>();

        idleState.AddTransition(new PlayerIdleTransition(this));
        walkState.AddTransition(new PlayerMoveTransition(this));
    }

    void Start()
    {
        // TODO : temp
        speed = 10f;
        stateMachine.Initialize(idleState);
    }
}
