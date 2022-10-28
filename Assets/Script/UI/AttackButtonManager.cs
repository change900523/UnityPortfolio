using System.Collections.Generic;
using UnityEngine;

public class AttackButtonManager : MonoBehaviour
{
    [SerializeField]
    private List<AttackButton> buttons = null;

    private void Awake()
    {
        EventManager<AttackData[]>.Instance.StartListening(EventEnum.SetAttackButton, SetAttackButton);
        EventManager<int>.Instance.StartListening(EventEnum.ReserveAttack, ReserveAttack);
        EventManager<int>.Instance.StartListening(EventEnum.CancelAttack, CancelAttack);
        EventManager<int>.Instance.StartListening(EventEnum.ExecuteAttack, ExecuteAttack);
        EventManager<List<(int, float)>>.Instance.StartListening(EventEnum.UpdateAttackCoolTime, UpdateCoolTime);
    }

    private void OnDestroy()
    {
        EventManager<AttackData[]>.Instance.StopListening(EventEnum.SetAttackButton, SetAttackButton);
        EventManager<int>.Instance.StopListening(EventEnum.ReserveAttack, ReserveAttack);
        EventManager<int>.Instance.StopListening(EventEnum.CancelAttack, CancelAttack);
        EventManager<int>.Instance.StopListening(EventEnum.ExecuteAttack, ExecuteAttack);
        EventManager<List<(int, float)>>.Instance.StopListening(EventEnum.UpdateAttackCoolTime, UpdateCoolTime);
    }

    public void SetAttackButton(AttackData[] datas)
    {
        int count = Mathf.Min(datas.Length, buttons.Count);

        for (int i = 0; i < count; i++)
        {
            buttons[i].Initialize(datas[i]);
        }
    }

    private void CancelAttack(int index)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].attackIndex == index)
            {
                buttons[i].SetCancelState();
                break;
            }
        }
    }

    private void ReserveAttack(int index)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].attackIndex == index)
            {
                buttons[i].SetReserveState();
                break;
            }
        }
    }

    private void ExecuteAttack(int index)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].attackIndex == index)
            {
                buttons[i].SetCoolTimeState();
                break;
            }
        }
    }

    private void UpdateCoolTime(List<(int, float)> datas)
    {
        int count = Mathf.Min(datas.Count, buttons.Count);

        for (int i = 0; i < count; i++)
        {
            if (buttons[i].attackIndex == datas[i].Item1)
            {
                buttons[i].UpdateCoolTime(datas[i].Item2);
            }
        }
    }
}
