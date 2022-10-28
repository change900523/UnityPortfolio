using System.Collections.Generic;


public class Parallel : CompositeBase
{
    private readonly Dictionary<ITask, TaskStatus> _childStatus = new Dictionary<ITask, TaskStatus>();
    protected override TaskStatus OnUpdate()
    {
        int successCount = 0;
        int failureCount = 0;

        for (int i = 0; i < Children.Count; i++)
        {
            TaskStatus prevStatus;
            if (_childStatus.TryGetValue(Children[i], out prevStatus) && prevStatus == TaskStatus.Success)
            {
                successCount++;
                continue;
            }

            TaskStatus status = Children[i].Update(deltaTime);
            _childStatus[Children[i]] = status;

            switch (status)
            {
                case TaskStatus.Failure:
                    failureCount++;
                    break;
                case TaskStatus.Success:
                    successCount++;
                    break;
            }
        }

        if (successCount == Children.Count)
        {
            End();
            return TaskStatus.Success;
        }

        if (failureCount > 0)
        {
            End();
            return TaskStatus.Failure;
        }

        return TaskStatus.Continue;
    }

    public override void Reset()
    {
        _childStatus.Clear();

        base.Reset();
    }

    public override void End()
    {
        for (int i = 0; i < Children.Count; i++)
        {
            Children[i].End();
        }
    }
}

