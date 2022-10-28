public abstract class AttackManager
{
    protected AttackInfo[] attackInfos = null;

    public abstract void Update(float deltaTime);
    public abstract void ExecuteAttack(int attackIndex);

    public virtual void RegistAttackInfo(AttackData[] attackDatas)
    {
        attackInfos = new AttackInfo[attackDatas.Length];

        int[] attackButtonInfos = new int[attackDatas.Length];

        if (attackDatas != null)
        {
            for (int i = 0; i < attackDatas.Length; i++)
            {
                attackInfos[i] = new AttackInfo(attackDatas[i]);
                attackButtonInfos[i] = attackDatas[i].Index;
            }
        }
    }

    public void ResetCoolTime()
    {
        for (int idx = 0; idx < attackInfos.Length; ++idx)
        {
            attackInfos[idx].ElapsedTime = 0;
            attackInfos[idx].IsActive = true;
        }
    }
}


public class AttackInfo
{
    public AttackInfo(AttackData data)
    {
        Index = data.Index;
        DamRangeValue1 = data.DamRangeValue1;
        DamRangeValue2 = data.DamRangeValue2;
        MaxRange = data.MaxRange;
        HitType = data.HitType;
        CoolTime = data.CoolTime;
        RangeTime = data.RangeTime;
        AttackTime = data.AttackTime;
        HitTimes = data.HitTimes;
        ElapsedTime = 0f;
        AnimationIndex = data.AnimationIndex;
        AttackDamage = data.AttackDamage;
        IsActive = true;
    }

    public int Index { get; private set; }
    public float DamRangeValue1 { get; set; }
    public float DamRangeValue2 { get; set; }
    public float MaxRange { get; private set; }
    public EHitType HitType { get; set; }
    public float CoolTime { get; private set; }
    public float RangeTime { get; private set; }
    public float AttackTime { get; private set; }
    public float[] HitTimes { get; private set; }
    public float ElapsedTime { get; set; }
    public int AnimationIndex { get; private set; }
    public bool IsActive { get; set; }
    public float AttackDamage { get; private set; }
}
