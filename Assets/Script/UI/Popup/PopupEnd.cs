using UnityEngine.SceneManagement;

public class PopupEnd : PopupBase
{
    public void OnClickEndButton()
    {
        PopupManager.Instance.ClosePopupUI(this);
        SceneManager.LoadScene("Menu");
    }
}
