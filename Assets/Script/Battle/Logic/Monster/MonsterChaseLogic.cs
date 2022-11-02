using UnityEngine;
using UnityEngine.AI;

public class MonsterChaseLogic : MoveLogic
{
    public MonsterChaseLogic(MonsterBehaviorTreeData data, Animator inAnimator, NavMeshAgent agent) : base(data, inAnimator, agent) 
    {
        treeData = data;
    }

    private readonly MonsterBehaviorTreeData treeData = null;

    public override void StartLogic()
    {
        base.StartLogic();
        Initialize(treeData.ChaseData.stopDistance, Defines.MONSTER_MOVE_SPEED);
    }

    public override TaskStatus OnUpdateLogic(float deltaTime)
    { 
        return Move(deltaTime, treeData.Target.transform.position);
    }

    protected override void Cancel()
    {
        treeData.ChaseData = MoveData.Default;
    }

    protected override void End()
    {
        treeData.ChaseData = MoveData.Default;
    }

    protected override bool IsCancel()
    {
        return treeData.CancelChase();
    }
}
