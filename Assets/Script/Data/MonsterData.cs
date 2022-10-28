using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Data/Monster Data", order = 1)]
public class MonsterData : ScriptableObject
{
    [SerializeField]
    private uint tribe = 0;
    public uint Trive { get => tribe; }

    [SerializeField]
    private float attack = 0f;
    public float Attack { get => attack; }

    [SerializeField]
    private float hp = 0f;
    public float HP { get => hp; }

    [SerializeField]
    private float aggroDistance = 5f;
    public float AggroDistance { get => aggroDistance; }

    [SerializeField]
    private float comeBackDistance = 3f;
    public float ComeBackDistance { get => comeBackDistance; }
}
