public abstract class SuperState : State
{
    public StateMachine subStateMachine;

    protected SuperState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        subStateMachine = new StateMachine();
    }

    public override void Enter()
    {
        base.Enter();
        subStateMachine.Initialize(GetInitialState());
        subStateMachine.CurrentState.Enter();
    }

    public override void Update()
    {
        base.Update();
        subStateMachine.CurrentState.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        subStateMachine.CurrentState.FixedUpdate();
    }

    public override void Exit()
    {
        subStateMachine.CurrentState.Exit();
        base.Exit();
    }

    protected abstract State GetInitialState();

    public void ChangeSubState(State nextSubState)
    {
        subStateMachine.ChangeState(nextSubState);
    }


}
