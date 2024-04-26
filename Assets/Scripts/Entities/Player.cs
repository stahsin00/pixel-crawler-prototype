public class Player : Entity
{
    public InputHandler inputHandler;

    public PlayerJumpState jumpState;

    protected override void Awake()
    {
        base.Awake();

        inputHandler = GetComponent<InputHandler>();

        jumpState = new PlayerJumpState(this, stateMachine);

        idleState.AddTransition(new PlayerIdleTransition(this));
        walkState.AddTransition(new PlayerMoveTransition(this));
        jumpState.AddTransition(new PlayerJumpFallTransition(this));

        PlayerJumpTransition playerJumpTransition= new PlayerJumpTransition(this);
        idleState.AddTransition(playerJumpTransition);
        walkState.AddTransition(playerJumpTransition);
    }

    void Start()
    {
        // TODO : temp
        speed = 10f;
        stateMachine.Initialize(idleState);
    }
}
