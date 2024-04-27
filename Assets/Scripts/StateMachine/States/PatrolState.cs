public class PatrolState : SuperState
{
    public LookAroundState lookAroundState;
    public MarchState marchState;

    public PatrolState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        lookAroundState = new LookAroundState(entity,subStateMachine);
        marchState = new MarchState(entity,subStateMachine);

        lookAroundState.AddTransition(new PatrolMarchTransition(entity, this));
        marchState.AddTransition(new PatrolLookAroundTransition(entity, this));
    }

    

    protected override State GetInitialState()
    {
        return lookAroundState;
    }
}
