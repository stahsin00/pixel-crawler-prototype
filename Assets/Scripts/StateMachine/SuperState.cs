public abstract class SuperState : State
{
    protected StateMachine subStateMachine;

    protected SuperState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        subStateMachine = new StateMachine();
    }

    public override void Enter()
    {
        base.Enter();
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
        base.Exit();
        subStateMachine.CurrentState.Exit();
    }

    public void ChangeSubState(State nextSubState)
    {
        subStateMachine.ChangeState(nextSubState);
    }


}
