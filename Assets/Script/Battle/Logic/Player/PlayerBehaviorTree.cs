using System;

public class PlayerBehaviorTree : BehaviorTreeObject
{
    public PlayerBehaviorTree(PlayerBehaviorTreeData inTreeData,
                            Action inDieStart,
                            Action inControlMoveStart,
                            Func<float, TaskStatus> inControlMoveUpdate,
                            Action inAttackChaseStart,
                            Func<float, TaskStatus> inAttackChaseUpdate,
                            Action inAttackStart,
                            Func<float, TaskStatus> inAttackUpdate,
                            Func<TaskStatus> inIdleUpdate)
    {
        treeData = inTreeData;
        dieStart = inDieStart;
        controlMoveStart = inControlMoveStart;
        controlMoveUpdate = inControlMoveUpdate;
        attackChaseStart = inAttackChaseStart;
        attackChaseUpdate = inAttackChaseUpdate;
        attackAnimationStart = inAttackStart;
        attackAnimationUpdate = inAttackUpdate;
        idleUpdate = inIdleUpdate;
    }

    private readonly PlayerBehaviorTreeData treeData = null;
    private readonly Action dieStart = null;
    private readonly Action controlMoveStart = null;
    private readonly Func<float, TaskStatus> controlMoveUpdate = null;
    private readonly Action attackChaseStart = null;
    private readonly Func<float, TaskStatus> attackChaseUpdate = null;
    private readonly Action attackAnimationStart = null;
    private readonly Func<float, TaskStatus> attackAnimationUpdate = null;
    private readonly Func<TaskStatus> idleUpdate = null;

    public void SetTree()
    {
        tree = new BehaviorTreeBuilder()
            .AddSelector()
                .AddSequence()
                    .AddCondition(() => treeData.IsDie)
                    .AddDo(dieStart)
                .End()
                .AddSequence()
                    .AddCondition(() => treeData.IsControlling())
                    .AddWaitTime(controlMoveStart, controlMoveUpdate)
                .End()
                .AddSequence()
                    .AddCondition(() => treeData.IsAttack())
                    .AddSelector()
                        .AddSequence()
                            .AddCondition(() => treeData.IsAttackChase())
                            .AddWaitTime(attackChaseStart, attackChaseUpdate)
                        .End()
                        .AddWaitTime(attackAnimationStart, attackAnimationUpdate)
                    .End()
                .End()
                .AddSequence()
                    .AddCondition(() => treeData.IsIdle())
                    .AddDo(idleUpdate)
                .End()
            .Build();
    }
}