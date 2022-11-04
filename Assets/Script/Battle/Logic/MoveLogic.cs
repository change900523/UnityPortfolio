using UnityEngine;
using UnityEngine.AI;

public abstract class MoveLogic : BehaviorTreeDeltaTimeLogic
{
    public MoveLogic(BehaviorTreeData data, Animator inAnimator, NavMeshAgent agent)
    {
        treeData = data;
        animator = inAnimator;
        navMeshAgent = agent;
    }

    private readonly BehaviorTreeData treeData = null;
    private readonly int hashRun = Animator.StringToHash("Run");
    private readonly Animator animator = null;
    private readonly NavMeshAgent navMeshAgent = null;
    private float stopDistance = float.MaxValue;
    private Transform transform = null;
    private float speed = 0f;

    protected void Initialize(float inStopDistance, float inSpeed)
    {
        animator.SetBool(hashRun, true);
        transform = treeData.transform;
        stopDistance = inStopDistance * inStopDistance;
        speed = inSpeed;
    }

    protected TaskStatus Move(float deltaTime, Vector3 targetPosition)
    {
        TaskStatus result = TaskStatus.Continue;

        if (IsCancel() == true)
        {
            Cancel();
            animator.SetBool(hashRun, false);
            result = TaskStatus.Success;
        }
        else
        {
            float targetDistance = (transform.position - targetPosition).sqrMagnitude;
            NavMeshPath navMeshPath = new NavMeshPath();
            navMeshAgent.CalculatePath(targetPosition, navMeshPath);

            if (navMeshPath.status == NavMeshPathStatus.PathComplete && targetDistance > stopDistance)
            {
                Vector3 destination = Vector3.zero;

                for (int i = 0; i < navMeshPath.corners.Length; i++)
                {
                    if (navMeshPath.corners[i].x != transform.position.x && navMeshPath.corners[i].z != transform.position.z)
                    {
                        destination = new Vector3(navMeshPath.corners[i].x, 0f, navMeshPath.corners[i].z);
                        break;
                    }
                }

                transform.position = Vector3.MoveTowards(transform.position, destination, deltaTime * speed);
                navMeshAgent.nextPosition = transform.position;

                Vector3 targetVector = destination - transform.position;
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
        }

        return result;
    }

    protected abstract void Cancel();
    protected abstract void End();
    protected abstract bool IsCancel();
}
