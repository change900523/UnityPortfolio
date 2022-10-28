

public class RepeatUntilSuccess : DecoratorBase
{
    protected override TaskStatus OnUpdate()
    {
        if (Child.Update(deltaTime) == TaskStatus.Success)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Continue;
    }
}
