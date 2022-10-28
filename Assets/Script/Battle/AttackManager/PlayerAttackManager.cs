using System;
using System.Collections.Generic;

public class PlayerAttackManager : AttackManager
{
    public override void Update(float deltaTime)
    {
        List<(int, float)> coolTimeData = new List<(int, float)>();

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

                coolTimeData.Add((info.Index, info.ElapsedTime));
            }
        }

        EventManager<List<(int, float)>>.Instance.TriggerEvent(EventEnum.UpdateAttackCoolTime, coolTimeData);
    }

    public override void RegistAttackInfo(AttackData[] attackDatas)
    {
        base.RegistAttackInfo(attackDatas);
        EventManager<AttackData[]>.Instance.TriggerEvent(EventEnum.SetAttackButton, attackDatas);
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

            EventManager<int>.Instance.TriggerEvent(EventEnum.ExecuteAttack, attackIndex);
        }
    }

    public AttackInfo GetActiveAttackInfoByIndex(int attackIndex)
    {
        AttackInfo info = Array.Find(attackInfos, inner => inner.IsActive == true && inner.Index == attackIndex);
        if (info != null)
        {
            return info;
        }

        return null;
    }

}

