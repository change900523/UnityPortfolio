using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeGameLogic : GameLogic
{
    [SerializeField]
    protected List<Vector3> monsterSpawnPosition = new List<Vector3>();

    private List<Monster> monsters = new List<Monster>();
    private Monster targetMonster = null;
  

    protected override void InitEnemy()
    {
        foreach (Vector3 postion in monsterSpawnPosition)
        {
            GameObject gameObject = Instantiate(monsterObj, postion, Quaternion.Euler(Vector3.zero));
            Monster monster = gameObject.GetComponent<Monster>();
            monster.Initialize(player, postion, Attack);
            battleObjects.Add(monster);
            monsters.Add(monster);
        }
    }

    protected override BattleObject AutoTargetAmongEnemy(float autoTargetDistance)
    {
        Monster monster = null;

        float minDistance = autoTargetDistance * autoTargetDistance;

        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].IsDie() == false)
            {
                float distance = (player.transform.position - monsters[i].transform.position).sqrMagnitude;

                if (minDistance > distance)
                {
                    minDistance = distance;
                    monster = monsters[i];
                }
            }
        }

        if (monster != null)
        {
            if (monster.IsTarget == false)
            {
                if (targetMonster != null)
                {
                    targetMonster.SetTarget(false);
                }

                monster.SetTarget(true);
            }
        }
        else
        {
            if (targetMonster != null)
            {
                targetMonster.SetTarget(false);
            }
        }

        targetMonster = monster;

        return monster;
    }
}
