public class Player : Entity
{
    public InputHandler inputHandler;

    public PlayerJumpState jumpState;
    public PlayerAttackState attackState;

    protected override void Awake()
    {
        base.Awake();

        inputHandler = GetComponent<InputHandler>();

        jumpState = new PlayerJumpState(this, stateMachine);
        attackState = new PlayerAttackState(this, stateMachine, attackPos);

        idleState.AddTransition(new PlayerIdleTransition(this));
        walkState.AddTransition(new PlayerMoveTransition(this));
        jumpState.AddTransition(new PlayerJumpFallTransition(this));

        PlayerJumpTransition playerJumpTransition= new PlayerJumpTransition(this);
        idleState.AddTransition(playerJumpTransition);
        walkState.AddTransition(playerJumpTransition);

        idleState.AddTransition(new PlayerAttackTransition(this));
        attackState.AddTransition(new PlayerFinishAttackTransition(this));
    }

    void Start()
    {
        // TODO : temp
        speed = 10f;
        stateMachine.Initialize(idleState);
    }
}
