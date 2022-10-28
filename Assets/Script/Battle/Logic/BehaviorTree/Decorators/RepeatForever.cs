

public class RepeatForever : DecoratorBase
{
    protected override TaskStatus OnUpdate()
    {
        Child.Update(deltaTime);
        return TaskStatus.Continue;
    }
}
