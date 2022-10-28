using System;


public class DecoratorGeneric : DecoratorBase
{
    public Func<ITask, TaskStatus> updateLogic;

    protected override TaskStatus OnUpdate()
    {
        if (updateLogic != null)
        {
            return updateLogic(Child);
        }

        return Child.Update(deltaTime);
    }
}
