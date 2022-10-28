using System;



public class ActionGeneric : ActionBase
{
    public Func<TaskStatus> updateLogic = null;
    public Action startLogic = null;
    public Action initLogic = null;
    public Action exitLogic = null;

    protected override TaskStatus OnUpdate()
    {
        if (updateLogic != null)
        {
            return updateLogic();
        }

        return TaskStatus.Continue;
    }

    protected override void OnStart()
    {
        startLogic?.Invoke();
    }

    protected override void OnExit()
    {
        exitLogic?.Invoke();
    }

    protected override void OnInit()
    {
        initLogic?.Invoke();
    }
}
