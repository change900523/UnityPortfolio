


public abstract class GenericTaskBase
{
    protected float deltaTime = 0f;

    public virtual TaskStatus Update(float inDeltaTime)
    {
        deltaTime = inDeltaTime;

        return TaskStatus.Success;
    }
}
