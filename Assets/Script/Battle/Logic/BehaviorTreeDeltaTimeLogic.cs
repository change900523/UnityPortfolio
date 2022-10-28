
public class BehaviorTreeDeltaTimeLogic : BehaviorTreeLogic
{
    public TaskStatus UpdateLogic(float deltaTime)
    {
        TaskStatus result = OnUpdateLogic(deltaTime);

        return result;
    }

    public virtual TaskStatus OnUpdateLogic(float deltaTime)
    {
        return TaskStatus.Continue;
    }
}