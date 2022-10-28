using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Data/Attack Data", order = 1)]
public class AttackData : ScriptableObject
{
    [SerializeField]
    private int index = Defines.DEFAULT_ATTACK_INDEX;
    public int Index { get => index; }

    [SerializeField]
    private string skillName = string.Empty;
    public string SkillName { get => skillName; }

    [SerializeField]
    private float damRangeValue1;
    public float DamRangeValue1 { get => damRangeValue1; }


    [SerializeField]
    private float damRangeValue2;
    public float DamRangeValue2 { get => damRangeValue2; }

    [SerializeField]
    private float maxRange;
    public float MaxRange { get => maxRange; }
    
    [SerializeField]
    private EHitType hitType;
    public EHitType HitType { get => hitType; }

    [SerializeField]
    private float coolTime;
    public float CoolTime { get => coolTime; }
    
    [SerializeField]
    private float rangeTime;
    public float RangeTime { get => rangeTime; }
    
    [SerializeField]
    private float attackTime;
    public float AttackTime { get => attackTime; }
    
    [SerializeField]
    private float[] hitTimes;
    public float[] HitTimes { get => hitTimes; }

    [SerializeField]
    private int animationIndex;
    public int AnimationIndex { get => animationIndex; }

    [SerializeField]
    private float attackDamage;
    public float AttackDamage { get => attackDamage; }
}
