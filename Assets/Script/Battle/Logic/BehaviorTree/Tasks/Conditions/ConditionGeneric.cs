using System;

public class ConditionGeneric : ConditionBase
{
    public Func<bool> updateLogic = null;
    public Action startLogic = null;
    public Action initLogic = null;
    public Action exitLogic = null;

    protected override bool OnUpdate()
    {
        if (updateLogic != null)
        {
            return updateLogic();
        }

        return true;
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