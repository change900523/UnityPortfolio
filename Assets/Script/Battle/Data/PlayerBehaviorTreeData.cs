using UnityEngine;

public class PlayerBehaviorTreeData : BehaviorTreeData
{
    public PlayerBehaviorTreeData(Transform inTransform) : base(inTransform) { }


    public Vector2 JoystickDirection { get; set; } = Vector2.zero;
    public bool IsTarget() => Target != null && Target.IsDie() == false;

    public bool IsControlling()
    {
        return JoystickDirection != Vector2.zero;
    }

    public bool CancelControlMove()
    {
        bool result = IsDie || JoystickDirection == Vector2.zero || ReserveAttackInfo != null;

        if (result == true)
        {
            JoystickDirection = Vector2.zero;
        }

        return result;
    }

    public bool IsAttack()
    {
        return ReserveAttackInfo != null;
    }

    public bool IsAttackChase()
    {
        float targetDistance = (transform.position - Target.transform.position).sqrMagnitude;
        return targetDistance > ReserveAttackInfo.MaxRange * ReserveAttackInfo.MaxRange;
    }

    public bool CancelAttackChase()
    {
        return IsDie || IsControlling();
    }

    public override bool CancelAttackAnimation()
    {
        bool result = IsDie || IsControlling();

        if (result == true)
        {
            ReserveAttackInfo = null;
        }

        return result;
    }

    public bool IsIdle()
    {
        return true;
    }

    public bool CancelIdle()
    {
        return IsDie || IsAttack() || IsControlling();
    }
}
