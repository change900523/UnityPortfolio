
using UnityEngine;

public class PlayerDieLogic : BehaviorTreeLogic
{
    public PlayerDieLogic(Animator inAnimator)
    {
        animator = inAnimator;
    }

    private readonly Animator animator = null;
    private readonly int hashDie = Animator.StringToHash("Die");

    public override void StartLogic()
    {
        base.StartLogic();
        animator.SetBool(hashDie, true);
    }
}