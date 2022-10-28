using System.Collections.Generic;
using UnityEngine;

public class AttackTestGameLogic : GameLogic
{
    [SerializeField]
    private float x = 0;
    [SerializeField]
    private float z = 0;
    [SerializeField]
    private Transform hitParent = null;
    [SerializeField]
    private ChangeAttackInfo changeAttackInfo = null;

    private List<HitTest> hitTests = new List<HitTest>();
    private HitTest target = null;

    protected override void InitEnemy()
    {
        for (float i = 0; i <= x; i += 0.2f)
        {
            for (float j = 0; j <= z; j += 0.2f)
            {
                GameObject gameObject = Instantiate(monsterObj, new Vector3(i, 0f, j), Quaternion.Euler(Vector3.zero), hitParent);
                gameObject.name = "( " + i.ToString() + ", " + j.ToString() + " )";
                HitTest hitTest = gameObject.GetComponent<HitTest>();
                hitTests.Add(hitTest);
                battleObjects.Add(hitTest);
            }
        }
    }

    protected override BattleObject AutoTargetAmongEnemy(float autoTargetDistance)
    {
        HitTest hitTest = null;

        float minDistance = autoTargetDistance * autoTargetDistance;

        for (int i = 0; i < hitTests.Count; i++)
        {
            if (hitTests[i].IsDie() == false)
            {
                float distance = (player.transform.position - hitTests[i].transform.position).sqrMagnitude;

                if (minDistance > distance)
                {
                    minDistance = distance;
                    hitTest = hitTests[i];
                }
            }
        }

        if (hitTest != null)
        {
            if (hitTest.IsTarget == false)
            {
                if (target != null)
                {
                    target.SetTarget(false);
                }

                hitTest.SetTarget(true);
            }
        }
        else
        {
            if (target != null)
            {
                target.SetTarget(false);
            }
        }

        target = hitTest;

        return hitTest;
    }

    protected override void Attack(AttackInfo attackInfo, uint attackerTribe, BattleObject attacker, BattleObject target, float damage)
    {
        (EHitType hitTpye, float value1, float value2) info = changeAttackInfo.GetAttackInfo();

        attackInfo.HitType = info.hitTpye;
        attackInfo.DamRangeValue1 = info.value1;
        attackInfo.DamRangeValue2 = info.value2;


        base.Attack(attackInfo, attackerTribe, attacker, target, damage);
    }
}
