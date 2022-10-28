using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private float ANGLE = 30f;

    [SerializeField]
    private RectTransform back = null;
    [SerializeField]
    private RectTransform stick = null;

    private float radius = 0f;
    private List<(float, float)> angleRanges = new List<(float, float)>();
    private int angleIndex = -1;
    private readonly int DEFAULT_ANGLE_INDEX = -1;

    public void Start()
    {
        radius = back.rect.width * 0.45f;

        angleIndex = DEFAULT_ANGLE_INDEX;

        int count = (int)(360f / ANGLE);
        float halfAngle = ANGLE * 0.5f;

        for (int i = 0; i < count; i++)
        {
            float angle = ANGLE * i;
            angleRanges.Add((angle - halfAngle, angle + halfAngle));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)back.position;
        value = Vector2.ClampMagnitude(value, radius);
        stick.localPosition = value;
        value = value.normalized;
        SetDirection(value);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick.anchoredPosition = Vector2.zero;

        SetDirection(Vector2.zero);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    private void SetDirection(Vector2 inValue)
    {
        if (inValue == Vector2.zero)
        {
            angleIndex = DEFAULT_ANGLE_INDEX;
            EventManager<Vector2>.Instance.TriggerEvent(EventEnum.JoystickControl, Vector2.zero);
        }
        else
        {
            float angle = Quaternion.FromToRotation(inValue, Vector3.up).eulerAngles.z;
            for (int i = 0; i < angleRanges.Count; i++)
            {
                if (angleIndex != i && angle >= angleRanges[i].Item1 && angle < angleRanges[i].Item2)
                {
                    angleIndex = i;
                    Transform camera = Camera.main.transform;
                    Vector3 forwardDirection = new Vector3(camera.forward.x, 0, camera.forward.z);
                    Vector3 direction = Quaternion.AngleAxis(ANGLE * i, Vector3.up) * forwardDirection;
                    direction = direction.normalized;
                    Vector2 joystickDirection = new Vector2(direction.x, direction.z);

                    EventManager<Vector2>.Instance.TriggerEvent(EventEnum.JoystickControl, joystickDirection);

                    //gameLogicBase.SetJoystickDirection(joystickDirection.x, joystickDirection.y);
                    break;
                }
            }
        }
    }

#if UNITY_EDITOR
    private Vector2 keyInput = Vector2.zero;

    private void Update()
    {
        InputForEditor();
    }

    public void InputForEditor()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input != Vector2.zero)
        {
            input = input.normalized;
            if (input != keyInput)
            {
                keyInput = input;
                SetDirection(keyInput);
            }
        }
        else if (keyInput != Vector2.zero && input == Vector2.zero)
        {
            keyInput = Vector2.zero;
            SetDirection(Vector2.zero);
        }
    }
#endif
}
