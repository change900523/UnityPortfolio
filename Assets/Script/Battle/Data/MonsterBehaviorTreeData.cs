using UnityEngine;

public class MonsterBehaviorTreeData : BehaviorTreeData
{
    public MonsterBehaviorTreeData(Transform inTransform,
                                    float inAggroDistance,
                                    MonsterAttackManager inAttackManager, 
                                    Vector3 inBasePosition, 
                                    float inComeBackDistance) : base(inTransform)
    {
        AggroDistance = inAggroDistance;
        attackManager = inAttackManager;
        basePostion = inBasePosition;
        comeBackDistance = inComeBackDistance;
    }

    public float AggroDistance { get; private set; } = 0f;
    public MoveData ChaseData { get; set; } = MoveData.Default;
    public MoveData ComeBackData { get; set; } = MoveData.Default;

    private readonly MonsterAttackManager attackManager = null;
    private readonly Vector3 basePostion = Vector3.one;
    private readonly float comeBackDistance = 0f;

    public bool IsChase()
    {
        AttackInfo attackInfo = attackManager.GetAttackInfo();
        float maxRange = Mathf.Pow(attackInfo.MaxRange, 2);
        if (maxRange < TargetDistance())
        {
            ChaseData = new MoveData(true, attackInfo.MaxRange);
        }

        return ChaseData.isMove;
    }

    public bool CancelChase()
    {
        bool result = false;

        if (IsDie == true || TargetDistance() > Mathf.Pow(AggroDistance, 2))
        {
            result = true;
        }

        return result;
    }

    public bool IsAttack()
    {
        bool result = false;

        if (Target.IsDie() == false)
        {
            float targetDistance = TargetDistance();
            if (AggroDistance >= targetDistance)
            {
                AttackInfo active = attackManager.GetActiveAttackInfo();
                if (active != null)
                {
                    result = true;

                    float maxRange = Mathf.Pow(active.MaxRange, 2);
                    if (maxRange < targetDistance)
                    {
                        ChaseData = new MoveData(true, active.MaxRange);
                    }
                    else
                    {
                        ReserveAttackInfo = active;
                    }
                }
            }
        }

        return result;
    }

    public bool NotAttackChase()
    {
        return !ChaseData.isMove;
    }

    public override bool CancelAttackAnimation()
    {
        return IsDie == true;
    }

    public bool IsComeBack()
    {
        if ((basePostion - transform.position).sqrMagnitude > Mathf.Pow(comeBackDistance, 2) == true)
        {
            ComeBackData = new MoveData(true, 0f);
        }

        return ComeBackData.isMove;
    }

    public bool CancelComeBack()
    {
        return IsDie == true;
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