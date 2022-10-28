using UnityEngine;

public abstract class PopupBase : MonoBehaviour
{
    [SerializeField]
    private Canvas rootCanvas = null;

    private EPopupType popupType;
    
    public virtual void Initialize(EPopupType inPopupType, int order)
    {
        popupType = inPopupType;

        rootCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        rootCanvas.overrideSorting = true;
        rootCanvas.sortingOrder = order;
    }
}
