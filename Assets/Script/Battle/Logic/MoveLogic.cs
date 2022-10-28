using UnityEngine;

public abstract class MoveLogic : BehaviorTreeDeltaTimeLogic
{
    public MoveLogic(BehaviorTreeData data, Animator inAnimator)
    {
        treeData = data;
        animator = inAnimator;
    }

    private readonly BehaviorTreeData treeData = null;
    private readonly int hashRun = Animator.StringToHash("Run");
    private readonly Animator animator = null;
    private float stopDistance = float.MaxValue;
    private Transform transform = null;
    private float speed = 0f;

    protected void Initialize(float inStopDistance, float inSpeed)
    {
        animator.SetBool(hashRun, true);
        transform = treeData.transform;
        stopDistance = inStopDistance;
        speed = inSpeed;
    }

    protected TaskStatus Move(float deltaTime, Vector3 targetPosition)
    {
        TaskStatus result = TaskStatus.Continue;
        float targetDistance = (transform.position - targetPosition).sqrMagnitude;

        if (IsCancel() == true)
        {
            Cancel();
            animator.SetBool(hashRun, false);
            result = TaskStatus.Success;
        }
        else if (targetDistance > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, deltaTime * speed);

            Vector3 targetVector = targetPosition - transform.position;
            targetVector = new Vector3(targetVector.x, 0f, targetVector.z);

            if (targetVector != Vector3.zero && targetVector.magnitude > Mathf.Epsilon)
            {
                Quaternion lookRotation = Quaternion.LookRotation(targetVector);
                Quaternion quaternion = Quaternion.Lerp(transform.rotation, lookRotation, deltaTime * Defines.ROTATE_SPEED);
                transform.rotation = quaternion;
            }
        }
        else
        {
            End();
            animator.SetBool(hashRun, false);
            result = TaskStatus.Success;
        }

        return result;
    }

    protected abstract void Cancel();
    protected abstract void End();
    protected abstract bool IsCancel();
}
