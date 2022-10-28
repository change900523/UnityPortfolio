using System;



public class WaitTime : ActionBase
{
    public Func<float, TaskStatus> updateLogic;
    public Action startLogic;
    protected override TaskStatus OnUpdate()
    {
        TaskStatus result = TaskStatus.Success;

        if (updateLogic != null)
        {
            result = updateLogic(deltaTime);
        }

        return result;
    }

    protected override void OnStart()
    {
        startLogic?.Invoke();
    }
}

