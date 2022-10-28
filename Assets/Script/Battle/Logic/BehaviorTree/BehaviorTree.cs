using System.Collections.Generic;



public interface IBehaviorTree
{
    TaskRoot Root { get; }
    int TickCount { get; }

    void AddActiveTask(ITask task);
    void RemoveActiveTask(ITask task);
}

[System.Serializable]
public class BehaviorTree : IBehaviorTree
{
    private readonly List<ITask> _tasks = new List<ITask>();
    public int TickCount { get; private set; }

    public TaskRoot Root { get; } = new TaskRoot();

    public BehaviorTree()
    {
        SyncNodes(Root);
    }

    public TaskStatus Tick(float inDeltaTime)
    {
        TaskStatus status = Root.Update(inDeltaTime);
        if (status != TaskStatus.Continue)
        {
            Reset();
        }

        return status;
    }

    public void Reset()
    {
        for (int i = 0; i < _tasks.Count; i++)
        {
            _tasks[i].End();
        }

        _tasks.Clear();
        TickCount++;
    }

    public void AddNode(ITaskParent parent, ITask child)
    {
        parent.AddChild(child);
        child.ParentTree = this;
    }

    public void Splice(ITaskParent parent, BehaviorTree tree)
    {
        parent.AddChild(tree.Root);

        SyncNodes(tree.Root);
    }

    private void SyncNodes(ITaskParent taskParent)
    {
        taskParent.ParentTree = this;

        for (int i = 0; i < taskParent.Children.Count; i++)
        {
            taskParent.Children[i].ParentTree = this;

            ITaskParent childInterface = taskParent.Children[i] as ITaskParent;
            if (childInterface != null)
            {
                SyncNodes(childInterface);
            }
        }
    }

    public void AddActiveTask(ITask task)
    {
        _tasks.Add(task);
    }

    public void RemoveActiveTask(ITask task)
    {
        _tasks.Remove(task);
    }
}
