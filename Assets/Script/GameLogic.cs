using Cinemachine;
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
    private CinemachineVirtualCamera playerCamera = null;

    protected Player player = null;
    protected List<BattleObject> battleObjects = new List<BattleObject>();


    private void Awake()
    {
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

        playerCamera.Follow = gameObject.transform;
        playerCamera.LookAt = gameObject.transform;
    }

    protected virtual void Attack(AttackInfo attackInfo, uint attackerTribe, BattleObject attacker, BattleObject target, float damage)
    {
        Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.z);
        Vector2 attackPosition = new Vector2(attacker.transform.position.x, attacker.transform.position.z);
        Vector2 direction = targetPosition - attackPosition;
        direction = direction.normalized;

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
                SectorFormCast(attackerTribe, targetPosition, direction, attackInfo.DamRangeValue1, attackInfo.DamRangeValue2, damage);
                break;
            case EHitType.Box:
                BoxCast(attackerTribe, attackPosition, new Vector2(attackInfo.DamRangeValue1 * 0.5f, attackInfo.DamRangeValue2 * 0.5f), direction, damage);
                break;
            default:
                break;
        }
    }

    private void CircleCast(uint attackerTribe, Vector2 center, float radius, float damage)
    {
        if (radius != 0)
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
    }

    private void SectorFormCast(uint attackerTribe, Vector2 center, Vector2 direction, float radius, float standardDegree, float damage)
    {
        if (radius != 0 && standardDegree != 0)
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
    }

    private void BoxCast(uint attackerTribe, Vector2 start, Vector2 boxSize, Vector2 direction, float damage)
    {
        if (boxSize != Vector2.zero)
        {
            float left = -boxSize.x;
            float right = boxSize.x;
            float top = boxSize.y;
            float bottom = -boxSize.y;
            float cos = direction.x;
            float sin = direction.y;

            for (int i = 0; i < battleObjects.Count; i++)
            {
                if (battleObjects[i].Tribe != attackerTribe && IsObjectBattle(battleObjects[i]))
                {
                    Vector2 position = new Vector2(battleObjects[i].transform.position.x, battleObjects[i].transform.position.z);
                    float radius = battleObjects[i].ColliderRadius;

                    if (IsPointInBox(left - radius, right + radius, bottom, top, sin, cos, right, start, direction, position))
                    {
                        battleObjects[i].TakeDamage(damage);
                    }
                    else if (IsPointInBox(left, radius, bottom - radius, top + radius, sin, cos, right, start, direction, position))
                    {
                        battleObjects[i].TakeDamage(damage);
                    }
                    else
                    {
                        Vector2 vertex1 = new Vector2((left * cos) - (top * sin), (left * sin) + (top * cos));
                        Vector2 vertex2 = new Vector2((right * cos) - (top * sin), (right * sin) + (top * cos));
                        Vector2 vertex3 = new Vector2((right * cos) - (bottom * sin), (right * sin) + (bottom * cos));
                        Vector2 vertex4 = new Vector2((left * cos) - (bottom * sin), (left * sin) + (bottom * cos));

                        Vector2 movePostion = start + direction * right;
                        vertex1 += movePostion;
                        vertex2 += movePostion;
                        vertex3 += movePostion;
                        vertex4 += movePostion;

                        if (IsPointInCircle(vertex1, position, radius) ||
                            IsPointInCircle(vertex2, position, radius) ||
                            IsPointInCircle(vertex3, position, radius) ||
                            IsPointInCircle(vertex4, position, radius)) //               
                        {
                            battleObjects[i].TakeDamage(damage);
                        }
                    }
                }
            }
        }
    }

    private bool IsPointInBox(float left,
                    float right,
                    float bottom,
                    float top,
                    float sin,
                    float cos,
                    float length,
                    Vector2 start,
                    Vector2 direction,
                    Vector2 point)
    {
        bool result = false;

        Vector2 vertex1 = new Vector2((left * cos) - (top * sin), (left * sin) + (top * cos));
        Vector2 vertex2 = new Vector2((right * cos) - (top * sin), (right * sin) + (top * cos));
        //Vector2 vertex3 = new Vector2((right * cos) - (bottom * sin), (right * sin) + (bottom * cos));
        Vector2 vertex4 = new Vector2((left * cos) - (bottom * sin), (left * sin) + (bottom * cos));

        Vector2 movePostion = start + direction * length;
        vertex1 += movePostion;
        vertex2 += movePostion;
        //vertex3 += movePostion;
        vertex4 += movePostion;

        //Debug.Log("boxsize = " + left + ", " + top
        //        + "\n direction = " + direction.x + ", " + direction.y
        //        + "\n movePosition = " + movePostion.x + ", " + movePostion.y
        //        + "\n start = " + start.x + ", " + start.y
        //        + "\n vertex1 = " + vertex1.x + ", " + vertex1.y
        //        + "\n vertex2 = " + vertex2.x + ", " + vertex2.y
        //        + "\n vertex3 = " + vertex3.x + ", " + vertex3.y
        //        + "\n vertex4 = " + vertex4.x + ", " + vertex4.y
        //        + "\n point = " + point.x + ", " + point.y);

        Vector2 checkLine1 = vertex2 - vertex1;
        Vector2 checkLine2 = vertex4 - vertex1;

        point -= vertex1;
        float checkVector21 = point.x * checkLine1.x + point.y * checkLine1.y;
        if (0 <= checkVector21 && checkVector21 <= checkLine1.sqrMagnitude)
        {
            float checkVector22 = point.x * checkLine2.x + point.y * checkLine2.y;

            if (0 <= checkVector22 && checkVector22 <= checkLine2.sqrMagnitude)
            {
                result = true;
            }
        }

        return result;
    }

    private bool IsPointInCircle(Vector2 vertex, Vector2 point, float radius)
    {
        Vector2 delta = vertex - point;
        return delta.sqrMagnitude <= (radius * radius);
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
