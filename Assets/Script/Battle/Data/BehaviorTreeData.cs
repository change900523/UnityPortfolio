using UnityEngine;

public abstract class BehaviorTreeData
{
    public BehaviorTreeData(Transform inTransform)
    {
        transform = inTransform;
    }

    public Transform transform { get; private set; } = null;
    public AttackInfo ReserveAttackInfo { get; set; } = null;
    public BattleObject Target { get; set; } = null;
    public bool IsDie { get; set; } = false;


    public abstract bool CancelAttackAnimation();
    public float TargetDistance() => (transform.position - Target.transform.position).sqrMagnitude;
    public void LookAtTarget() => transform.LookAt(Target.transform);
}
