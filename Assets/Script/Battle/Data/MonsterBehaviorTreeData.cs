using UnityEngine;

public class MonsterBehaviorTreeData : BehaviorTreeData
{
    public MonsterBehaviorTreeData(Transform inTransform, float inAggroDistance) : base(inTransform)
    {
        AggroDistance = inAggroDistance;
    }

    public float AggroDistance { get; private set; } = 0f;
    public MoveData ChaseData { get; set; } = MoveData.Default;
    public MoveData ComeBackData { get; set; } = MoveData.Default;

    public bool IsChase()
    {
        return ChaseData.isMove;
    }

    public bool CancelChase()
    {
        bool result = false;

        if (TargetDistance() > Mathf.Pow(AggroDistance, 2))
        {
            result = true;
        }

        return result;
    }

    public bool IsAttack()
    {
        return ReserveAttackInfo != null;
    }

    public override bool CancelAttackAnimation()
    {
        return false;
    }

    public bool IsComeBack()
    {
        return ComeBackData.isMove;
    }

    public bool CancelComeBack()
    {
        return false;
    }

    public bool IsIdle()
    {
        return true;
    }
}


public struct MoveData
{
    public MoveData(bool inIsMove, float inStopDistance)
    {
        isMove = inIsMove;
        stopDistance = inStopDistance;
    }

    public bool isMove;
    public float stopDistance;

    public static MoveData Default { get { return new MoveData(false, 0f); } }
}