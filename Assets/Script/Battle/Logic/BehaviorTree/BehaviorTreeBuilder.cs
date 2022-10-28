using System;
using System.Collections.Generic;


public class BehaviorTreeBuilder
{
    private readonly BehaviorTree _tree;
    private readonly List<ITaskParent> _pointers = new List<ITaskParent>();

    private ITaskParent PointerCurrent
    {
        get
        {
            if (_pointers.Count == 0)
            {
                return null;
            }

            return _pointers[_pointers.Count - 1];
        }
    }


    public BehaviorTreeBuilder()
    {
        _tree = new BehaviorTree();
        _pointers.Add(_tree.Root);
    }

    public BehaviorTreeBuilder ParentTask<P>() where P : ITaskParent, new()
    {
        P parent = new P();

        return AddNodeWithPointer(parent);
    }

    public BehaviorTreeBuilder Decorator(Func<ITask, TaskStatus> logic)
    {
        DecoratorGeneric decorator = new DecoratorGeneric
        {
            updateLogic = logic,
        };

        return AddNodeWithPointer(decorator);
    }

    public BehaviorTreeBuilder AddNodeWithPointer(ITaskParent task)
    {
        AddNode(task);
        _pointers.Add(task);

        return this;
    }

    public BehaviorTreeBuilder AddInverter()
    {
        return ParentTask<Inverter>();
    }

    public BehaviorTreeBuilder AddReturnSuccess()
    {
        return ParentTask<ReturnSuccess>();
    }

    public BehaviorTreeBuilder AddReturnFailure()
    {
        return ParentTask<ReturnFailure>();
    }

    public BehaviorTreeBuilder AddRepeatUntilSuccess()
    {
        return ParentTask<RepeatUntilSuccess>();
    }

    public BehaviorTreeBuilder AddRepeatUntilFailure()
    {
        return ParentTask<RepeatUntilFailure>();
    }

    public BehaviorTreeBuilder AddRepeatForever()
    {
        return ParentTask<RepeatForever>();
    }

    public BehaviorTreeBuilder AddSequence()
    {
        return ParentTask<Sequence>();
    }

    public BehaviorTreeBuilder AddSelector()
    {
        return ParentTask<Selector>();
    }

    /// <summary>
    /// Selects the first node to return success
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public BehaviorTreeBuilder AddSelectorRandom()
    {
        return ParentTask<SelectorRandom>();
    }

    public BehaviorTreeBuilder AddParallel()
    {
        return ParentTask<Parallel>();
    }

    public BehaviorTreeBuilder AddDo(Action action)
    {
        return AddNode(new ActionGeneric
        {
            startLogic = action
        });
    }

    public BehaviorTreeBuilder AddDo(Func<TaskStatus> action)
    {
        return AddNode(new ActionGeneric
        {
            updateLogic = action
        });
    }

    public BehaviorTreeBuilder AddDo(Action action, Func<TaskStatus> action1)
    {
        return AddNode(new ActionGeneric
        {
            startLogic = action,
            updateLogic = action1
        });
    }

    /// <summary>
    /// Return continue until time has passed
    /// </summary>
    /// <param name="name"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public BehaviorTreeBuilder AddWaitTime(Func<float, TaskStatus> action)
    {
        return AddNode(new WaitTime()
        {
            updateLogic = action
        });
    }

    public BehaviorTreeBuilder AddWaitTime(Action action, Func<float, TaskStatus> action1)
    {
        return AddNode(new WaitTime()
        {
            startLogic = action,
            updateLogic = action1,
        });
    }

    public BehaviorTreeBuilder AddCondition(Func<bool> action)
    {
        return AddNode(new ConditionGeneric
        {
            updateLogic = action
        });
    }

    public BehaviorTreeBuilder AddWait(int turns = 1)
    {
        return AddNode(new Wait
        {
            turns = turns
        });
    }

    public BehaviorTreeBuilder AddNode(ITask node)
    {
        _tree.AddNode(PointerCurrent, node);
        return this;
    }

    public BehaviorTreeBuilder Splice(BehaviorTree tree)
    {
        _tree.Splice(PointerCurrent, tree);

        return this;
    }

    public BehaviorTreeBuilder End()
    {
        _pointers.RemoveAt(_pointers.Count - 1);

        return this;
    }

    public BehaviorTree Build()
    {
        return _tree;
    }
}