
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public void ChangeScene(string scene)
    {
        GameManager.Instance.ChangeScene(scene);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
