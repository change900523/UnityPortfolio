


public abstract class BehaviorTreeLogic
{
    public virtual void StartLogic()
    {
    }

    public TaskStatus UpdateLogic()
    {
        return OnUpdateLogic();
    }

    public virtual TaskStatus OnUpdateLogic()
    {
        return TaskStatus.Continue;
    }
}
