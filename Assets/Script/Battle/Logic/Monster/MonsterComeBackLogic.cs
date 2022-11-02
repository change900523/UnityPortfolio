using UnityEngine;
using UnityEngine.AI;

public class MonsterComeBackLogic : MoveLogic
{
    public MonsterComeBackLogic(MonsterBehaviorTreeData data, Animator inAnimator, Vector3 inBasePosition, NavMeshAgent agent) : base(data, inAnimator, agent)
    {
        treeData = data;
        basePosition = inBasePosition;
    }

    private readonly MonsterBehaviorTreeData treeData = null;
    private readonly Vector3 basePosition = Vector3.zero;

    public override void StartLogic()
    {
        base.StartLogic();
        Initialize(0f, Defines.MONSTER_MOVE_SPEED);
    }

    public override TaskStatus OnUpdateLogic(float deltaTime)
    {
        return Move(deltaTime, basePosition);
    }

    protected override void Cancel()
    {
        treeData.ComeBackData = MoveData.Default;
    }

    protected override void End()
    {
        treeData.ComeBackData = MoveData.Default;
    }

    protected override bool IsCancel()
    {
        return treeData.CancelComeBack();
    }
}
