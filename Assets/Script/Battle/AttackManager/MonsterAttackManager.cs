using System;

public class MonsterAttackManager : AttackManager
{
    public override void Update(float deltaTime)
    {
        for (int idx = 0; idx < attackInfos.Length; ++idx)
        {
            AttackInfo info = attackInfos[idx];

            if (info.CoolTime != 0 && info.IsActive == false)
            {
                float cooltime = info.CoolTime;
                if (cooltime < 0)
                {
                    cooltime = 0;
                }

                info.ElapsedTime += deltaTime;
                if (info.ElapsedTime >= cooltime)
                {
                    info.ElapsedTime = cooltime;
                    info.IsActive = true;
                }
            }
        }
    }

    public override void ExecuteAttack(int attackIndex)
    {
        AttackInfo findInfo = Array.Find(attackInfos, inner => inner.Index == attackIndex);
        if (findInfo != null)
        {
            findInfo.ElapsedTime = 0;

            if (findInfo.CoolTime > 0)
            {
                findInfo.IsActive = false;
            }
        }
    }

    public AttackInfo GetActiveAttackInfo()
    {
        AttackInfo result = null;

        AttackInfo[] actives = Array.FindAll(attackInfos, inner => inner.IsActive);

        if (actives.Length > 0)
        {
            if (actives.Length > 1)
            {
                Array.Sort(actives, SortAttack);
            }

            result = actives[0];
        }

        return result;
    }

    public AttackInfo GetAttackInfo()
    {
        AttackInfo result = null;

        Array.Sort(attackInfos, SortAttack);
        result = attackInfos[0];

        return result;
    }

    private int SortAttack(AttackInfo src1, AttackInfo src2)
    {
        return src1.MaxRange.CompareTo(src2.MaxRange) * -1;
    }
}

