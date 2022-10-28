using System.Collections.Generic;



public interface ITaskParent : ITask
{
    List<ITask> Children { get; }

    ITaskParent AddChild(ITask child);
}
