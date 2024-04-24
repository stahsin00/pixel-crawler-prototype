public abstract class Transition
{
    protected Entity entity;

    public Transition(Entity entity)
    {
        this.entity = entity;
    }

    public abstract bool ShouldTransition();

    public abstract State NextState();
}
