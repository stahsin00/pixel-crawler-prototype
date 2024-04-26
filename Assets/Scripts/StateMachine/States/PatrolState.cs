public class PatrolState : SuperState
{
    public LookAroundState lookAroundState;
    public MarchState marchState;

    public PatrolState(Entity entity, StateMachine stateMachine) : base(entity, stateMachine)
    {
        lookAroundState = new LookAroundState(entity,stateMachine);
        marchState = new MarchState(entity,stateMachine);
    }
}
