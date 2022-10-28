using System;


public class MonsterBehaviorTree : BehaviorTreeObject
{
    public MonsterBehaviorTree(MonsterBehaviorTreeData inTreeData,
                           Action inDieStart,
                           Action inChaseStart,
                           Func<float, TaskStatus> inChaseUpdate,
                           Action inAttackStart,
                           Func<float, TaskStatus> inAttackUpdate,
                           Action inComeBackStart,
                           Func<float, TaskStatus> inComeBackUpdate,
                           Func<TaskStatus> inIdleUpdate)
    {
        treeData = inTreeData;
        dieStart = inDieStart;
        chaseStart = inChaseStart;
        chaseUpdate = inChaseUpdate;
        attackAnimationStart = inAttackStart;
        attackAnimationUpdate = inAttackUpdate;
        comeBackStart = inComeBackStart;
        comeBackUpdate = inComeBackUpdate;
        idleUpdate = inIdleUpdate;
    }

    private readonly MonsterBehaviorTreeData treeData = null;
    private readonly Action dieStart = null;
    private readonly Action chaseStart = null;
    private readonly Func<float, TaskStatus> chaseUpdate = null;
    private readonly Action attackAnimationStart = null;
    private readonly Func<float, TaskStatus> attackAnimationUpdate = null;
    private readonly Action comeBackStart = null;
    private readonly Func<float, TaskStatus> comeBackUpdate = null;
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
                    .AddCondition(() => treeData.IsAttack())
                     .AddWaitTime(attackAnimationStart, attackAnimationUpdate)
                .End()
                .AddSequence()
                    .AddCondition(() => treeData.IsChase())
                    .AddWaitTime(chaseStart, chaseUpdate)
                .End()
                .AddSequence()
                    .AddCondition(() => treeData.IsComeBack())
                    .AddWaitTime(comeBackStart, comeBackUpdate)
                .End()
                .AddSequence()
                    .AddCondition(() => treeData.IsIdle())
                    .AddDo(idleUpdate)
                .End()
            .Build();
    }
}
