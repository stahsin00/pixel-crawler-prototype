public abstract class SubStateTransiton : Transition
{
    protected SuperState superState;

    protected SubStateTransiton(Entity entity, SuperState superState) : base(entity)
    {
        this.superState = superState;
    }
}
