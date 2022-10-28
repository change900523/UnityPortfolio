

public class Selector : CompositeBase
{
    protected override TaskStatus OnUpdate()
    {
        for (int i = ChildIndex; i < Children.Count; i++)
        {
            ITask child = Children[ChildIndex];

            switch (child.Update(deltaTime))
            {
                case TaskStatus.Success:
                    return TaskStatus.Success;
                case TaskStatus.Continue:
                    return TaskStatus.Continue;
            }

            ChildIndex++;
        }

        return TaskStatus.Failure;
    }
}
