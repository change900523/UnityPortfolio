

public class Sequence : CompositeBase
{
    protected override TaskStatus OnUpdate()
    {
        for (int i = ChildIndex; i < Children.Count; i++)
        {
            ITask child = Children[ChildIndex];

            TaskStatus status = child.Update(deltaTime);
            if (status != TaskStatus.Success)
            {
                return status;
            }

            ChildIndex++;
        }

        return TaskStatus.Success;
    }
}
