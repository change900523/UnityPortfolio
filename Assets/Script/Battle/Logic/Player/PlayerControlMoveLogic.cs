using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControlMoveLogic : BehaviorTreeDeltaTimeLogic
{
    public PlayerControlMoveLogic(PlayerBehaviorTreeData data,  Animator inAnimator, Func<BattleObject> inAutoTargetfunc, NavMeshAgent agent)
    {
        treeData = data;
        animator = inAnimator;
        autoTargetFunc = inAutoTargetfunc;
        navMeshAgent = agent;
    }

    private readonly int hashRun = Animator.StringToHash("Run");
    private readonly PlayerBehaviorTreeData treeData = null;
    private readonly Animator animator = null;
    private readonly Func<BattleObject> autoTargetFunc = null;
    private readonly NavMeshAgent navMeshAgent = null;

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
            Transform transform = treeData.transform;

            Vector3 joystickDirection = new Vector3(treeData.JoystickDirection.x, 0f, treeData.JoystickDirection.y);
            if (joystickDirection != Vector3.zero && joystickDirection.magnitude > Mathf.Epsilon)
            {
                Quaternion lookRotation = Quaternion.LookRotation(joystickDirection);
                Quaternion quaternion = Quaternion.Lerp(transform.rotation, lookRotation, deltaTime * Defines.ROTATE_SPEED);
                transform.rotation = quaternion;
            }

            Vector3 targetPosition = Vector3.MoveTowards(transform.position, transform.position + joystickDirection, deltaTime * Defines.PLAYER_MOVE_SPEED);
            NavMeshPath navMeshPath = new NavMeshPath();
            navMeshAgent.CalculatePath(targetPosition, navMeshPath);
            
            if (navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                transform.position = targetPosition;
                navMeshAgent.nextPosition = transform.position;
            }

            treeData.Target = autoTargetFunc.Invoke();
        }

        return result;
    }
}
