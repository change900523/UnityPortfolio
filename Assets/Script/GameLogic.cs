using System.Collections.Generic;
using UnityEngine;

public abstract class GameLogic : MonoBehaviour
{
    [SerializeField]
    protected GameObject monsterObj = null;
    [SerializeField]
    private GameObject playerObj = null;
    [SerializeField]
    private GameObject battleUI = null;
    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private PlayerCamera playerCamera = null;

    protected Player player = null;
    protected List<BattleObject> battleObjects = new List<BattleObject>();


    private void Awake()
    {
        PopupManager.Instance.Initialize(canvas);

        InitUI();
        InitPlayer();
        InitEnemy();
    }

    protected abstract void InitEnemy();
    protected abstract BattleObject AutoTargetAmongEnemy(float autoTargetDistance);

    public void InitUI()
    {
        Instantiate(battleUI, canvas.transform);
    }

    private void InitPlayer()
    {
        GameObject gameObject = Instantiate(playerObj, Vector3.zero, Quaternion.Euler(Vector3.zero));

        player = gameObject.GetComponent<Player>();
        player.Initialize(AutoTargetAmongEnemy, Attack);
        battleObjects.Add(player);

        playerCamera.Initialize(gameObject.transform);
    }

    protected virtual void Attack(AttackInfo attackInfo, uint attackerTribe, BattleObject attacker, BattleObject target, float damage)
    {
        Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.z);
        Vector2 attackPosition = new Vector2(attacker.transform.position.x, attacker.transform.position.z);
        Vector2 direction = targetPosition - attackPosition;

        switch (attackInfo.HitType)
        {
            case EHitType.Guided:
                if (IsObjectBattle(target) == true)
                {
                    target.TakeDamage(damage);
                }
                break;
            case EHitType.Circle:
                CircleCast(attackerTribe, targetPosition, attackInfo.DamRangeValue1, damage);
                break;
            case EHitType.CircularSector:
                SectorFormCast(attackerTribe, targetPosition, attackInfo.DamRangeValue1, direction, attackInfo.DamRangeValue2, damage);
                break;
            default:
                break;
        }
    }

    private void CircleCast(uint attackerTribe, Vector2 center, float radius, float damage)
    {
        for (int i = 0; i < battleObjects.Count; i++)
        {
            if (battleObjects[i].Tribe != attackerTribe && IsObjectBattle(battleObjects[i]))
            {
                float objectRadius = battleObjects[i].ColliderRadius;

                float deltaX = battleObjects[i].transform.position.x - center.x;
                float deltaZ = battleObjects[i].transform.position.z - center.y;

                //UnityEngine.Debug.Log("index = " + data.index + ", 중심점 x = " + center.x + ", 중심점 x = " + center.y + ", 거리 = " + (deltaX * deltaX + deltaY * deltaY) + ", 반지름 = " + Math.Pow(radius + objectRadius, 2f));

                if ((deltaX * deltaX + deltaZ * deltaZ) < Mathf.Pow(radius + objectRadius, 2f))
                {
                    battleObjects[i].TakeDamage(damage);
                }
            }
        }
    }

    private void SectorFormCast(uint attackerTribe, Vector2 center, float radius, Vector2 direction, float standardDegree, float damage)
    {
        for (int i = 0; i < battleObjects.Count; i++)
        {
            if (battleObjects[i].Tribe != attackerTribe && IsObjectBattle(battleObjects[i]))
            {
                float objectRadius = battleObjects[i].ColliderRadius;

                Vector2 delta = new Vector2(battleObjects[i].transform.position.x, battleObjects[i].transform.position.z) - center;
                float sqr = delta.sqrMagnitude;

                if (sqr <= Mathf.Pow(radius + objectRadius, 2f))
                {
                    direction = direction.normalized;
                    delta = delta.normalized;
                    float degree;
                    if (delta == direction || delta == Vector2.zero)
                    {
                        degree = 0f;
                    }
                    else
                    {
                        float dotValue = Vector2.Dot(direction, delta);
                        degree = Mathf.Acos(dotValue) * Mathf.Rad2Deg;
                    }

                    if (degree >= standardDegree * -0.5f && degree <= standardDegree * 0.5f)
                    {
                        battleObjects[i].TakeDamage(damage);
                    }
                    //else
                    //{
                    //    Debug.Log("name = " + battleObjects[i].name + ", degree = " + degree);
                    //}
                }
                //else
                //{
                //    Debug.Log("name = " + battleObjects[i].name);
                //}
            }
        }
    }

    private bool IsObjectBattle(BattleObject battleObject)
    {
        return battleObject != null && battleObject.IsDie() == false;
    }
    public void OnClickEnd()
    {
        PopupManager.Instance.CreatePopupUI(EPopupType.PopupEnd);
    }
}
