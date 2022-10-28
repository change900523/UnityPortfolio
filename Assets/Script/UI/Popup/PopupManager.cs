using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    private Canvas uiRoot  = null;
    private int order = Defines.DEFAULT_UI_ORDER;
    private List<PopupBase> popupList = new List<PopupBase>();

    public void Initialize(Canvas canvas)
    {
        uiRoot = canvas;
        order = Defines.DEFAULT_UI_ORDER;
        popupList.Clear();
    }

    public PopupBase CreatePopupUI(EPopupType popupType)
    {
        string popupName = popupType.ToString();
        string popupPath = "Popup/" + popupName;

        PopupBase popup = Object.Instantiate(Resources.Load(popupPath, typeof(PopupBase))) as PopupBase;
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
