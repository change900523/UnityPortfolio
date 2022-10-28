using System.Collections.Generic;



public abstract class DecoratorBase : GenericTaskBase, ITaskParent
{
    public List<ITask> Children { get; } = new List<ITask>();

    public bool Enabled { get; set; } = true;

    public IBehaviorTree ParentTree { get; set; }
    public TaskStatus LastStatus { get; private set; }

    public ITask Child => Children.Count > 0 ? Children[0] : null;

    public override TaskStatus Update(float inDeltaTime)
    {
        base.Update(inDeltaTime);

        TaskStatus status = OnUpdate();
        LastStatus = status;

        return status;
    }

    protected abstract TaskStatus OnUpdate();

    public void End()
    {
        Child.End();
    }

    public void Reset()
    {
    }

    public ITaskParent AddChild(ITask child)
    {
        if (Child == null)
        {
            Children.Add(child);
        }

        return this;
    }
}
