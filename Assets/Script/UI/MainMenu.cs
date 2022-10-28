using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnClickBehaviorTree()
    {
        SceneManager.LoadScene("BehaviorTreeTest");
    }

    public void OnClickAttackTest()
    {
        SceneManager.LoadScene("AttackTest");
    }

    public void OnClickEnd()
    {
        Application.Quit();
    }
}
