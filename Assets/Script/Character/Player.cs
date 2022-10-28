using System;
using UnityEngine;

public class Player : BattleObject
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private AttackData[] attackDatas = null;
    [SerializeField]
    private PlayerData playerData = null;
    [SerializeField]
    private HPBar hpBar = null;


    private PlayerBehaviorTree behaviorTree = null;
    private PlayerBehaviorTreeData treeData = null;
    private PlayerAttackManager attackManager = new PlayerAttackManager();
    private Func<float, BattleObject> autoTargetFunc = null;
    private PlayerDieLogic dieLogic = null;
    private PlayerControlMoveLogic controlMoveLogic = null;
    private PlayerAttackChaseLogic attackChaseLogic = null;
    private AttackAnimationLogic attackAnimationLogic = null;
    private PlayerIdleLogic idleLogic = null;

    private void Awake()
    {
        AddEvent();
        Tribe = playerData.Trive;
        attack = playerData.Attack;
        hp = playerData.HP;
        hpBar.Initialize(hp);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        behaviorTree.Update(deltaTime);
        attackManager.Update(deltaTime);
    }

    private void OnDestroy()
    {
        RemoveEvent();
    }

    private void AddEvent()
    {
        EventManager<Vector2>.Instance.StartListening(EventEnum.JoystickControl, SetJoystickDirection);
        EventManager<int>.Instance.StartListening(EventEnum.ClickAttackButton, ReserveSkill);
    }

    private void RemoveEvent()
    {
        EventManager<Vector2>.Instance.StopListening(EventEnum.JoystickControl, SetJoystickDirection);
        EventManager<int>.Instance.StopListening(EventEnum.ClickAttackButton, ReserveSkill);
    }

    public void Initialize(Func<float, BattleObject> autoTarget, Action<AttackInfo, uint, BattleObject, BattleObject, float> inAttackAction)
    {
        autoTargetFunc = autoTarget;
        attackAction = inAttackAction;
        treeData = new PlayerBehaviorTreeData(transform);
        SetTree();
        attackManager.RegistAttackInfo(attackDatas);
    }

    private void SetTree()
    {
        dieLogic = new PlayerDieLogic(animator);
        controlMoveLogic = new PlayerControlMoveLogic(treeData, animator, AutoTarget);
        attackChaseLogic = new PlayerAttackChaseLogic(treeData, animator);
        attackAnimationLogic = new AttackAnimationLogic(treeData, animator, attackManager, Attack);
        idleLogic = new PlayerIdleLogic(treeData, AutoTarget);

        behaviorTree = new PlayerBehaviorTree(treeData,
                                    dieLogic.StartLogic,
                                    controlMoveLogic.StartLogic,
                                    controlMoveLogic.UpdateLogic,
                                    attackChaseLogic.StartLogic,
                                    attackChaseLogic.UpdateLogic,
                                    attackAnimationLogic.StartLogic,
                                    attackAnimationLogic.UpdateLogic,
                                    idleLogic.UpdateLogic);
        behaviorTree.SetTree();
    }

    private void SetJoystickDirection(Vector2 direction)
    {
        treeData.JoystickDirection = direction;
    }

    private void ReserveSkill(int attackIndex)
    {
        AttackInfo attackInfo = attackManager.GetActiveAttackInfoByIndex(attackIndex);

        if (treeData.IsTarget() == true && attackInfo != null)
        {
            treeData.ReserveAttackInfo = attackInfo;
            EventManager<int>.Instance.TriggerEvent(EventEnum.ReserveAttack, attackIndex);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        hpBar.SetHP(hp);
    }


    protected override void Die()
    {
        treeData.IsDie = true;
    }

    private BattleObject AutoTarget()
    {
        return autoTargetFunc(playerData.AutoTargetDistance);
    }
}
