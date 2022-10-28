

public class ReturnFailure : DecoratorBase
{
    protected override TaskStatus OnUpdate()
    {
        TaskStatus status = Child.Update(deltaTime);
        if (status == TaskStatus.Continue)
        {
            return status;
        }
        return TaskStatus.Failure;
    }
}
