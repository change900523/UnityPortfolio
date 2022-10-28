using System;
using UnityEngine;

public class PlayerControlMoveLogic : BehaviorTreeDeltaTimeLogic
{
    public PlayerControlMoveLogic(PlayerBehaviorTreeData data,  Animator inAnimator, Func<BattleObject> inAutoTargetfunc)
    {
        treeData = data;
        animator = inAnimator;
        autoTargetFunc = inAutoTargetfunc;
    }

    private readonly int hashRun = Animator.StringToHash("Run");
    private readonly PlayerBehaviorTreeData treeData = null;
    private readonly Animator animator = null;
    private readonly Func<BattleObject> autoTargetFunc = null;

    public override void StartLogic()
    {
        base.StartLogic();
        animator.SetBool(hashRun, true);
    }

    public override TaskStatus OnUpdateLogic(float deltaTime)
    {
        TaskStatus result = TaskStatus.Continue;

        if (treeData.CancelControlMove() == true)
        {
            animator.SetBool(hashRun, false);
            result = TaskStatus.Success;
        }
        else
        {
            Vector3 targetPosition = new Vector3(treeData.JoystickDirection.x, 0f, treeData.JoystickDirection.y);

            Transform transform = treeData.transform;

            transform.position = Vector3.MoveTowards(transform.position, transform.position + targetPosition , deltaTime * Defines.PLAYER_MOVE_SPEED);
            Quaternion lookRotation = Quaternion.LookRotation(targetPosition);
            Quaternion quaternion = Quaternion.Lerp(transform.rotation, lookRotation, deltaTime * Defines.ROTATE_SPEED);
            transform.rotation = quaternion;

            treeData.Target = autoTargetFunc.Invoke();
        }

        return result;
    }
}
