using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAttackInfo : MonoBehaviour
{
    [SerializeField]
    private Dropdown hitTypeDropdown = null;
    [SerializeField]
    private InputField value1InputField = null;
    [SerializeField]
    private InputField value2InputField = null;

    private EHitType hitType = EHitType.None;
    private float value1 = 0f;
    private float value2 = 0f;

    private void Start()
    {
        List<string> hitTypes = new List<string>();

        for (int i = 0; i < (int)EHitType.Max; i++)
        {
            EHitType hitType = (EHitType)i;
            hitTypes.Add(hitType.ToString());
        }

        hitTypeDropdown.ClearOptions();
        hitTypeDropdown.AddOptions(hitTypes);

        hitType = EHitType.Guided;

        value1InputField.text = value1.ToString();
        value2InputField.text = value2.ToString();
    }

    public void ChangeDropDown(int value)
    {
        hitType = (EHitType)value;
    }

    public void EndValue1InputField(InputField value)
    {
        value1 = float.Parse(value.text);
    }

    public void EndValue2InputField(InputField value)
    {
        value2 = float.Parse(value.text);
    }

    public (EHitType hitTpye, float value1, float value2) GetAttackInfo() => (hitType, value1, value2);
}
