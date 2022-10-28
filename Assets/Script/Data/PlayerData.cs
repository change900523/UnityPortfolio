using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data", order = 1)]
public class PlayerData : ScriptableObject
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
    private float autoTargetDistance = 0f;
    public float AutoTargetDistance { get => autoTargetDistance; }

}
