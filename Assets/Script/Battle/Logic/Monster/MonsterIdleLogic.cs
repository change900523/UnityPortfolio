using UnityEngine;

public class MonsterIdleLogic : BehaviorTreeLogic
{
    public MonsterIdleLogic(MonsterBehaviorTreeData data, MonsterAttackManager inAttackManager, Vector3 inBasePosition, float inComeBackDistance)
    {
        treeData = data;
        attackManager = inAttackManager;
        aggroDistance = Mathf.Pow(treeData.AggroDistance, 2);
        basePostion = inBasePosition;
        comeBackDistance = inComeBackDistance;
    }

    private readonly MonsterBehaviorTreeData treeData = null;
    private readonly MonsterAttackManager attackManager = null;
    private readonly float aggroDistance = 0f;
    private readonly Vector3 basePostion;
    private readonly float comeBackDistance;

    public override TaskStatus OnUpdateLogic()
    {
        TaskStatus result = TaskStatus.Continue;

        if (treeData.Target.IsDie() == false)
        {
            float targetDistance = treeData.TargetDistance();

            if (aggroDistance >= targetDistance)
            {
                AttackInfo active = attackManager.GetActiveAttackInfo();
                if (active != null)
                {
                    float maxRange = Mathf.Pow(active.MaxRange, 2);
                    if (maxRange < targetDistance)
                    {
                        treeData.ChaseData = new MoveData(true, active.MaxRange);
                    }
                    else
                    {
                        treeData.ReserveAttackInfo = active;
                    }

                    result = TaskStatus.Success;
                }
                else
                {
                    AttackInfo attackInfo = attackManager.GetAttackInfo();
                    float maxRange = Mathf.Pow(attackInfo.MaxRange, 2);
                    if (maxRange < targetDistance)
                    {
                        treeData.ChaseData = new MoveData(true, attackInfo.MaxRange);
                        result = TaskStatus.Success;
                    }
                }
            }
            else if (IsComeBack() == true)
            {
                treeData.ComeBackData = new MoveData(true, 0f);
                result = TaskStatus.Success;
            }
        }

        return result;
    }

    private bool IsComeBack() => (basePostion - treeData.transform.position).sqrMagnitude > Mathf.Pow(comeBackDistance, 2);
}
