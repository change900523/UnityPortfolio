using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    [SerializeField]
    private Slider coolTimeSlider = null;
    [SerializeField]
    private Text buttonName = null;

    private enum State
    {
        None,
        Idle,
        Reserve,
        CoolTime,
    }

    private State state = State.None;

    public int attackIndex { get; set; } = Defines.DEFAULT_ATTACK_INDEX;
    private float coolTime = 0f;

    public void Initialize(AttackData data)
    {
        buttonName.text = data.SkillName;
        coolTimeSlider.value = 0;
        attackIndex = data.Index;
        coolTime = data.CoolTime;
        state = State.Idle;
    }

    public void OnClick()
    {
        if (state == State.Idle)
        {
            EventManager<int>.Instance.TriggerEvent(EventEnum.ClickAttackButton, attackIndex);
        }
    }

    public void SetReserveState()
    {
        coolTimeSlider.value = 1f;
        state = State.Reserve;
    }

    public void SetCancelState()
    {
        coolTimeSlider.value = 0f;
        state = State.Idle;
    }
    public void SetCoolTimeState()
    {
        state = State.CoolTime;
    }

    public void UpdateCoolTime(float elapsedTime)
    {
        float time = coolTime - elapsedTime;

        if (time > 0f)
        {
            if (coolTimeSlider.gameObject.activeSelf == false)
            {
                coolTimeSlider.gameObject.SetActive(true);
            }

            coolTimeSlider.value = time / coolTime;
        }
        else
        {
            state = State.Idle;
            coolTimeSlider.value = 0f;
        }
    }
}
