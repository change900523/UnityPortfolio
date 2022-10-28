using System;


public class PlayerIdleLogic : BehaviorTreeLogic
{
    public PlayerIdleLogic(PlayerBehaviorTreeData data, Func<BattleObject> inAutoTargetfunc)
    {
        treeData = data;
        autoTargetFunc = inAutoTargetfunc;
    }

    private readonly PlayerBehaviorTreeData treeData = null;
    private readonly Func<BattleObject> autoTargetFunc = null;

    public override TaskStatus OnUpdateLogic()
    {
        TaskStatus result = TaskStatus.Continue;

        if (treeData.CancelIdle())
        {
            result = TaskStatus.Failure;
        }
        else if(treeData.IsTarget() == false)
        {
            treeData.Target = autoTargetFunc.Invoke();
        }

        return result;
    }
}
