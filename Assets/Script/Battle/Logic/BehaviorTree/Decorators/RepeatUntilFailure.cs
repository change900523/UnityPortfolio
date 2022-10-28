

public class RepeatUntilFailure : DecoratorBase
{
    protected override TaskStatus OnUpdate()
    {
        if (Child.Update(deltaTime) == TaskStatus.Failure)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Continue;
    }
}
