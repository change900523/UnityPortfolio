using System;
using UnityEngine;

public abstract class BattleObject : MonoBehaviour
{
    public float ColliderRadius { get; protected set; } = 0f;

    public uint Tribe { get; protected set; } = 0;
    protected Action<AttackInfo, uint, BattleObject, BattleObject, float> attackAction = null;
    protected float hp = 100f;
    protected float attack = 10f;


    public bool IsDie()
    {
        return hp <= 0;
    }

    public virtual void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            hp = 0f;
            Die();
        }
    }

    protected abstract void Die();

    protected void Attack(AttackInfo attackInfo, BattleObject target)
    {
        float damage = attack + attackInfo.AttackDamage;
        attackAction.Invoke(attackInfo, Tribe, this, target, damage);
    }
}
