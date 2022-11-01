using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoSingleton<PopupManager>
{
    [SerializeField]
    private Canvas uiRoot  = null;
    private int order = Defines.DEFAULT_UI_ORDER;
    private List<PopupBase> popupList = new List<PopupBase>();

    private void Start()
    {
        order = Defines.DEFAULT_UI_ORDER;
    }

    public PopupBase CreatePopupUI(EPopupType popupType)
    {
        string popupName = popupType.ToString();
        string popupPath = "Popup/" + popupName;

        PopupBase popup = Instantiate(Resources.Load(popupPath, typeof(PopupBase))) as PopupBase;
        popup.name = popupName;
        popup.transform.SetParent(uiRoot.transform);
        order++;
        popup.Initialize(popupType, order);

        popupList.Add(popup);

        return popup;
    }

    public void ClosePopupUI(PopupBase popup)
    {
        int findIdx = popupList.FindLastIndex(inner => inner == popup);
        if (findIdx >= 0)
        {
            Object.Destroy(popup.gameObject);
            popupList.RemoveAt(findIdx);
            order--;
        }
    }
}
