using System;
using UnityEngine;

public class AttackAnimationLogic : BehaviorTreeDeltaTimeLogic
{
    public AttackAnimationLogic(BehaviorTreeData data,
                                Animator inAniamtor,
                                AttackManager inAttackManager,
                                Action<AttackInfo, BattleObject> inAttackAction)
    {
        treeData = data;
        animator = inAniamtor;
        attackManager = inAttackManager;
        attackAction = inAttackAction;
    }

    private readonly BehaviorTreeData treeData = null;
    private readonly Animator animator = null;
    private readonly AttackManager attackManager = null;
    private readonly int hashAttackIndex = Animator.StringToHash("AttackIndex");
    private readonly Action<AttackInfo, BattleObject> attackAction = null;

    private float attackTime = 0f;
    private float[] hitTimes;
    private float hitTime;
    private int hitCount = 0;
    private bool isHit = false;
    private float attackDeltaTime = 0f;

    public override void StartLogic()
    {
        base.StartLogic();

        treeData.LookAtTarget();

        AttackInfo attackInfo = treeData.ReserveAttackInfo;

        animator.SetInteger(hashAttackIndex, attackInfo.AnimationIndex);

        attackManager.ExecuteAttack(attackInfo.Index);

        hitTimes = attackInfo.HitTimes;
        hitCount = 0;
        UpdateHitTime();
        attackTime = treeData.ReserveAttackInfo.AttackTime;
        attackDeltaTime = 0;
    }

    public override TaskStatus OnUpdateLogic(float deltaTime)
    {
        TaskStatus result = TaskStatus.Continue;

        if (treeData.CancelAttackAnimation())
        {
            animator.SetInteger(hashAttackIndex, Defines.DEFAULT_ATTACK_INDEX);
            treeData.ReserveAttackInfo = null;
            result = TaskStatus.Success;
        }
        else
        {
            attackDeltaTime += deltaTime;

            if (attackDeltaTime >= attackTime)
            {
                animator.SetInteger(hashAttackIndex, Defines.DEFAULT_ATTACK_INDEX);
                treeData.ReserveAttackInfo = null;
                result = TaskStatus.Success;
            }
            else if (isHit == false && attackDeltaTime >= hitTime)
            {
                attackAction.Invoke(treeData.ReserveAttackInfo, treeData.Target);
                UpdateHitTime();
            }
        }

        return result;
    }

    private void UpdateHitTime()
    {
        if (hitTimes.Length > hitCount)
        {
            hitTime = hitTimes[hitCount];
            hitCount++;
            isHit = false;
        }
        else
        {
            isHit = true;
        }
    }
}
