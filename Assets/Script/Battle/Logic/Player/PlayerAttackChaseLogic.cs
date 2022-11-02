using UnityEngine;
using UnityEngine.AI;

public class PlayerAttackChaseLogic : MoveLogic
{
    public PlayerAttackChaseLogic(PlayerBehaviorTreeData data, Animator inAnimator, NavMeshAgent agent) : base(data, inAnimator, agent)
    {
        treeData = data;
    }

    private readonly PlayerBehaviorTreeData treeData = null;

    public override void StartLogic()
    {
        base.StartLogic();
        Initialize(Mathf.Pow(treeData.ReserveAttackInfo.MaxRange, 2), Defines.PLAYER_MOVE_SPEED);
    }

    public override TaskStatus OnUpdateLogic(float deltaTime)
    {
        return Move(deltaTime, treeData.Target.transform.position);
    }

    protected override void Cancel()
    {
        EventManager<int>.Instance.TriggerEvent(EventEnum.CancelAttack, treeData.ReserveAttackInfo.Index);
        treeData.ReserveAttackInfo = null;
    }

    protected override void End()
    {
    }

    protected override bool IsCancel()
    {
        return treeData.CancelAttackChase();
    }
}