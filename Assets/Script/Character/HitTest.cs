using UnityEngine;

public class HitTest : BattleObject
{
    [SerializeField]
    private Renderer capsuleColor = null;

    public bool IsTarget { get; private set; } = false;

    private void Awake()
    {
        hp = 1f;
        Tribe = uint.MaxValue;
    }

    public void SetTarget(bool value)
    {
        if (IsDie() == false)
        {
            IsTarget = value;
            capsuleColor.material.color = IsTarget == true ? Color.yellow : Color.white;
        }
    }

    //public override void TakeDamage(float damage)
    //{

        //}

    protected override void Die()
    {
        capsuleColor.material.color = Color.clear;
    }
}
